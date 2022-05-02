using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Currency;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Currency
{
    public class CurrencyScannerComponent_Tests: ComponentTestModel<CurrencyScannerComponent>
    {
        #region Constants
        public const int AmountOnWallet = 40;
        public const int AmountRemoved = 10;
        public const int InsufficientAmount = 5;
        public const int FinalAmount = AmountOnWallet-AmountRemoved;
        #endregion
        #region Variables
        private ICurrency Currency;
        private CurrencyWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            Wallet = ScriptableObject.CreateInstance<CurrencyWallet>();
            Currency = ScriptableObject.CreateInstance<CurrencyData>();
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Currency = Currency;
            modelReference.Amount = AmountRemoved;
            modelReference.TaxWalletOnScan = true;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();
            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(Currency as CurrencyData);
            Wallet = null;
            Currency = null;
        }
        #endregion
        #region Test Preparation
        protected void InsertMoneyOnWallet(int amount)
        {
            Wallet.Change(Currency, amount);
        }
        #endregion
        #region Method Tests
        [Test]
        public void Amount_ReturnsAmountCriteriaOfComponent()
        {
            Assert.AreEqual(modelReference.Amount, AmountRemoved);
        }

        [Test]
        public void Check_ReturnsTrueWhenWalletHasEnoughResourcesAndComponentIsEnabled()
        {
            InsertMoneyOnWallet(AmountOnWallet);

            Assert.True(modelReference.Check(Wallet));
        }

        [Test]
        public void Check_ReturnsFalseWhenWalletCannotFulfillAmount()
        {
            InsertMoneyOnWallet(InsufficientAmount);

            Assert.False(modelReference.Check(Wallet));
        }

        [Test]
        public void Check_ReturnsFalseWhenComponentIsDisabled()
        {
            modelReference.enabled = false;

            Assert.False(modelReference.Check(Wallet));
        }

        [Test]
        public void Scan_TaxesWalletWhenEnoughCurrencyAndScannerCanTax()
        {
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.Scan(Wallet);

            Assert.AreEqual(FinalAmount, Wallet.AmountOf(Currency));
        }

        [Test]
        public void Scan_KeepsWalletValuesWhenNotEnoughCurrency()
        {
            InsertMoneyOnWallet(InsufficientAmount);
            modelReference.Scan(Wallet);

            Assert.AreEqual(InsufficientAmount, Wallet.AmountOf(Currency));
        }

        [Test]
        public void Scan_FiresScanEventWhenExecuted()
        {
            bool scanWasSuccessful = false;
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.onScanWallet += (walletHasEnough) => scanWasSuccessful = walletHasEnough;
            modelReference.Scan(Wallet);

            Assert.True(scanWasSuccessful);
        }

        [Test]
        public void Scan_KeepsWalletValuesWhenTaxingIsDisabled()
        {
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.TaxWalletOnScan = false;
            modelReference.Scan(Wallet);

            Assert.AreEqual(AmountOnWallet, Wallet.AmountOf(Currency));
        }

        [Test]
        public void ScanWallet_WorksTheSameAsScan()
        {
            modelReference.ScanWallet(Wallet);

            Assert.Zero(Wallet.AmountOf(Currency));

            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.ScanWallet(Wallet);

            Assert.AreEqual(FinalAmount, Wallet.AmountOf(Currency));
        }

        [Test]
        public void Tax_RemovesResourceFromTheWallet()
        {
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.Tax(Wallet);

            Assert.AreEqual(FinalAmount, Wallet.AmountOf(Currency));
        }

        [Test]
        public void Tax_FiresTaxEventWhenExecutedSucessfully()
        {
            int triggerCount = 0;
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.onTaxWallet += () => triggerCount++;
            modelReference.Tax(Wallet);

            Assert.AreEqual(1, triggerCount);
        }

        [Test]
        public void TaxWallet_WorksTheSameAsTax()
        {
            InsertMoneyOnWallet(AmountOnWallet);
            modelReference.TaxWallet(Wallet);

            Assert.AreEqual(FinalAmount, Wallet.AmountOf(Currency));
        }
        #endregion
    }
}