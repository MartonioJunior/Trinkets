using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Currencies;
using MartonioJunior.Trinkets;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Currencies
{
    public class ICurrencyWallet_Tests: TestModel<ICurrencyWallet>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Substitute<ICurrencyWallet>();
        }
        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_With()
        {
            const int Limit = 10000;
            var currency = Mock.ICurrency;
            var positiveValue = Range(0, Limit);
            var negativeValue = Range(-Limit, 0);
            var anyValue = Range(-Limit, Limit);

            yield return new object[]{currency, positiveValue, positiveValue};
            yield return new object[]{currency, negativeValue, 0};
            yield return new object[]{null, anyValue, 0};
        }
        [TestCaseSource(nameof(UseCases_With))]
        public void With_ChangesTheAmountOnWallet(ICurrency currency, int addedAmount, int resultAmount)
        {
            modelReference.Add(Arg.Any<IResourceData>()).Returns(true);
            modelReference.AmountOf(Arg.Any<ICurrency>()).Returns(resultAmount);

            Assert.AreEqual(modelReference, modelReference.With(currency, addedAmount));
            Assert.AreEqual(resultAmount, modelReference.AmountOf(currency));
        }
        #endregion
    }
}