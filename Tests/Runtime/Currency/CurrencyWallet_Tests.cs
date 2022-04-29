using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Currency;
using System.Collections.Generic;

namespace Tests.MartonioJunior.Collectables.Currency
{
    public class CurrencyWallet_Tests: ScrobTestModel<CurrencyWallet>
    {
        #region Constants
        public const int AmountOfCurrency = 70;
        public const int ChangeAmount = 20;
        public const int TotalAfterChange = AmountOfCurrency+ChangeAmount;
        private CurrencyData Currency;
        #endregion
        #region ScrobTestModel Implementation
        public override void CreateTestContext()
        {
            Currency = ScriptableObject.CreateInstance<CurrencyData>();
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
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
        public void Reset_SetsAmountForCurrencyOnWalletToZero()
        {
            modelReference.Reset(Currency);

            Assert.Zero(modelReference.AmountOf(Currency));
        }
        #endregion
    }
}