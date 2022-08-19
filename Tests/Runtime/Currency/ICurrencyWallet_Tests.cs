using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Currencies;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Currencies
{
    public class ICurrencyWallet_Tests: TestModel<ICurrencyWallet>
    {
        #region Constants
        public const int AmountOfCurrency = 20;
        public const int ChangeAmount = 50;
        private const string CurrencySymbol = "z";
        public const int TotalAfterChange = AmountOfCurrency+ChangeAmount;
        private CurrencyData Currency;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Currency);
            Currency.Symbol = CurrencySymbol;

            modelReference = EngineScrob.CreateInstance<CurrencyWallet>();
            modelReference.Change(Currency, AmountOfCurrency);
        }
        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate((Object)modelReference);
            ScriptableObject.DestroyImmediate(Currency);

            Currency = null;
            modelReference = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void With_ChangesTheAmountOnWallet()
        {
            modelReference.With(Currency,ChangeAmount);

            Assert.AreEqual(TotalAfterChange, modelReference.AmountOf(Currency));
        }

        [Test]
        public void With_DoesNothingWhenCurrencyIsNull()
        {
            modelReference.With(null,ChangeAmount);

            Assert.AreEqual(AmountOfCurrency, modelReference.AmountOf(Currency));
        }

        [Test]
        public void With_ReturnsTheWalletWhereTheChangeHappened()
        {
            Assert.AreEqual(modelReference, modelReference.With(Currency,ChangeAmount));
        }
        #endregion
    }
}