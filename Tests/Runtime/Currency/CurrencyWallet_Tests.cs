using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Currencies;
using System.Collections.Generic;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Currencies
{
    public class CurrencyWallet_Tests: ScrobTestModel<CurrencyWallet>
    {
        #region Constants
        public const int AmountOfCurrency = 70;
        public const int ChangeAmount = 20;
        private const string CurrencySymbol = "G";
        public const int TotalAfterChange = AmountOfCurrency+ChangeAmount;
        private const string WalletName = "Wallet";
        private CurrencyData Currency;
        #endregion
        #region ScrobTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Currency);
            Currency.Symbol = CurrencySymbol;

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.name = WalletName;
            modelReference.Change(Currency, AmountOfCurrency);
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(Currency);

            Currency = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_ReturnsTrueWhenInitializesNewCurrencyOnWallet()
        {
            modelReference.Clear();

            Assert.True(modelReference.Add(Currency));
        }

        [Test]
        public void Add_ReturnsFalseWhenCurrencyIsAlreadyInitialized()
        {
            Assert.False(modelReference.Add(Currency));
        }

        [Test]
        public void Add_ReturnsFalseWhenCurrencyIsNull()
        {
            Assert.False(modelReference.Add(null));
        }

        [Test]
        public void AmountOf_ReturnsAmountOfCurrencyInWallet()
        {
            Assert.AreEqual(AmountOfCurrency, modelReference.AmountOf(Currency));
        }

        [Test]
        public void AmountOf_ReturnsZeroWhenCurrencyNotOnWallet()
        {
            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(Currency));
        }

        [Test]
        public void AmountOf_ReturnsZeroWhenCurrencyIsNull()
        {
            Assert.Zero(modelReference.AmountOf(null));
        }

        [Test]
        public void Change_AdjustsValueOfCurrencyInWalletByDelta()
        {
            modelReference.Change(Currency, ChangeAmount);
            Assert.AreEqual(TotalAfterChange, modelReference.AmountOf(Currency));
        }

        [Test]
        public void Change_SetsNewPairWhenCurrencyKeyNotFound()
        {
            modelReference.Clear();

            modelReference.Change(Currency, ChangeAmount);
            Assert.AreEqual(ChangeAmount, modelReference.AmountOf(Currency));
        }

        [Test]
        public void Clear_RemovesAllEntriesFromWallet()
        {
            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(Currency));
        }

        [Test]
        public void DescribeContents_ShowsWalletContentsInStringFormat()
        {
            Assert.AreEqual($"{WalletName}: {AmountOfCurrency}({CurrencySymbol}) | ", modelReference.DescribeContents());
        }

        [Test]
        public void Remove_DeletesCurrencyFromWallet()
        {
            modelReference.Remove(Currency);

            Assert.Zero(modelReference.AmountOf(Currency));
        }

        [Test]
        public void Remove_ReturnsTrueWhenCurrencyIsRemovedSuccessfully()
        {
            Assert.True(modelReference.Remove(Currency));
        }

        [Test]
        public void Remove_ReturnsFalseWhenCurrencyNotOnWallet()
        {
            modelReference.Clear();

            Assert.False(modelReference.Remove(Currency));
        }

        [Test]
        public void Remove_ReturnsFalseWhenCurrencyIsNull()
        {
            Assert.False(modelReference.Remove(null));
        }

        [Test]
        public void Reset_SetsAmountForCurrencyOnWalletToZero()
        {
            modelReference.Reset(Currency);

            Assert.Zero(modelReference.AmountOf(Currency));
        }

        [Test]
        public void Reset_DoesNothingWhenCurrencyIsNull()
        {
            modelReference.Reset(null);

            Assert.NotZero(modelReference.AmountOf(Currency));
        }

        [Test]
        public void Search_ReturnsArrayOfResultsAligningWithPredicate()
        {
            EngineScrob.Instance(out CurrencyData failedCurrency);
            modelReference.Add(failedCurrency);
            var result = modelReference.Search((item) => {
                return modelReference.AmountOf(item) < ChangeAmount;
            });

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(failedCurrency, result[0]);

            ScriptableObject.DestroyImmediate(failedCurrency);
        }

        [Test]
        public void Search_ReturnsEmptyArrayWhenPredicateFails()
        {
            var result = modelReference.Search((item) => {
                return item == null;
            });

            Assert.Zero(result.Length);
        }

        [Test]
        public void Search_ReturnsAllElementsWhenPredicateIsNull()
        {
            var result = modelReference.Search(null);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Currency, result[0]);
        }
        #endregion
    }
}