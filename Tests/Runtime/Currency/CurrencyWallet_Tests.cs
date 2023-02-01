using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Currencies;
using System.Collections.Generic;
using MartonioJunior.Trinkets;
using NSubstitute;
using System;
using Random = UnityEngine.Random;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Currencies
{
    public class CurrencyWallet_Tests: ScrobTestModel<CurrencyWallet>
    {
        #region ScrobTestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Add()
        {
            ICurrency currency = Mock.ICurrency;

            yield return new object[]{ currency, Random.Range(1,10000), true };
            yield return new object[]{ null, Random.Range(1,10000), false };
            yield return new object[]{ currency, Random.Range(-10000,-1), false };
            yield return new object[]{ currency, 0, false };
        }
        [TestCaseSource(nameof(UseCases_Add))]
        public void Add_InsertResourcesOnWallet(ICurrency currency, int amount, bool output)
        {
            var data = new ResourceData(currency, amount);

            Assert.AreEqual(output, modelReference.Add(data));
        }

        public static IEnumerable UseCases_AmountOf()
        {
            ICurrency currency = Mock.ICurrency;
            var amount = Random.Range(1,10000);

            yield return new object[]{ currency, amount, amount };
            yield return new object[]{ currency, Random.Range(-10000, -1), 0 };
            yield return new object[]{ null, Random.Range(-10000,10000), 0 };
        }
        [TestCaseSource(nameof(UseCases_AmountOf))]
        public void AmountOf_ReturnsAmountOfCurrencyInWallet(ICurrency currency, int input, int output)
        {
            modelReference.Add(new ResourceData(currency, input));

            Assert.AreEqual(output, modelReference.AmountOf(currency));
        }

        public static IEnumerable UseCases_Change()
        {
            const int Limit = 1000;
            var positiveValue = Random.Range(0, Limit);
            var changeValue = Random.Range(-positiveValue, Limit);
            var biggerNegativeValue = Random.Range(-Limit, -positiveValue);

            yield return new object[]{ positiveValue, changeValue, positiveValue+changeValue };
            yield return new object[]{ positiveValue, biggerNegativeValue, 0 };
            yield return new object[]{ null, positiveValue, positiveValue};
            yield return new object[]{ null, biggerNegativeValue, 0};
        }
        [TestCaseSource(nameof(UseCases_Change))]
        public void Change_AdjustsValueOfCurrencyInWalletByDelta(int? input, int changeValue, int output)
        {
            ICurrency currency = Mock.ICurrency;
            if (input is int startAmount) modelReference.Add(new ResourceData(currency, startAmount));

            modelReference.Change(currency, changeValue);

            Assert.AreEqual(output, modelReference.AmountOf(currency));
        }

        [Test]
        public void Clear_RemovesAllEntriesFromWallet()
        {
            modelReference.Add(new ResourceData(Substitute(out ICurrency currency), 8));

            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(currency));
        }

        [Test]
        public void Contents_ReturnsCurrencyGroup()
        {
            Assert.IsInstanceOf(typeof(CurrencyGroup), modelReference.Contents);
            Assert.IsNotNull(modelReference.Contents);
        }

        public static IEnumerable UseCases_Remove()
        {
            const int Limit = 10000;
            var initialValue = Random.Range(1,Limit);
            var lowerAmount = Random.Range(1,initialValue);
            var higherAmount = Random.Range(initialValue,Limit);
            var anyValue = Random.Range(-Limit, Limit);

            yield return new object[]{ initialValue, lowerAmount, true, initialValue-lowerAmount};
            yield return new object[]{ initialValue, higherAmount, true, 0 };
            yield return new object[]{ initialValue, -lowerAmount, false, initialValue };
            yield return new object[]{ initialValue, 0, false, initialValue };
            yield return new object[]{ null, anyValue, false, 0 };
        }
        [TestCaseSource(nameof(UseCases_Remove))]
        public void Remove_DeletesCurrencyFromWallet(int? input, int removeAmount, bool operationResult, int finalValue)
        {
            ICurrency currency = Mock.ICurrency;
            if (input is int startAmount) modelReference.Add(new ResourceData(currency, startAmount));

            Assert.AreEqual(operationResult, modelReference.Remove(new ResourceData(currency, removeAmount)));
            Assert.AreEqual(finalValue, modelReference.AmountOf(currency));
        }

        public static IEnumerable UseCases_Reset_Currency()
        {
            yield return Mock.ICurrency;
            yield return null;
        }
        [Test]
        public void Reset_SetsAmountForCurrencyOnWalletToZero([ValueSource(nameof(UseCases_Reset_Currency))] ICurrency currency, [Random(-1000,1000,1)] int startAmount)
        {
            modelReference.Add(new ResourceData(currency, startAmount));

            modelReference.Reset(currency);

            Assert.Zero(modelReference.AmountOf(currency));
        }

        public static IEnumerable UseCases_Search()
        {
            var emptySource = Array<ResourceData>(0, null);
            var validSource = Array<ResourceData>(10, Mock.Currencies);

            Predicate<IResourceData> predicate = (item) => item.Amount > 1000;
            List<ResourceData> filteredData = new List<ResourceData>();
            foreach(var item in validSource)
                if (predicate(item)) filteredData.Add(item);

            yield return new object[]{ emptySource, predicate, emptySource };
            yield return new object[]{ emptySource, null, emptySource };
            yield return new object[]{ validSource, predicate, filteredData };
            yield return new object[]{ validSource, null, validSource };
        }
        [TestCaseSource(nameof(UseCases_Search))]
        public void Search_ReturnsArrayOfResultsAligningWithPredicate(ICollection<ResourceData> resources, Predicate<IResourceData> predicate, ICollection<ResourceData> output)
        {
            modelReference.AddRange(resources);

            var result = modelReference.Search(predicate);

            CollectionAssert.AreEquivalent(output, result);
        }
        #endregion
    }
}