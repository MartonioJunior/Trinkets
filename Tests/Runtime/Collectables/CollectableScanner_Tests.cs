using System.Collections;
using MartonioJunior.Collectables;
using MartonioJunior.Collectables.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MockCollectableScanner = MartonioJunior.Collectables.Collectables.CollectableScannerComponent;

namespace Tests.MartonioJunior.Collectables.Collectables
{
    public class MockCollectableScanner: CollectableScanner
    {
        #region Variables
        public bool taxedWallet = false;
        #endregion
        #region CollectableScanner Implementation
        public override bool FulfillsCriteria(ICollectableWallet wallet)
        {
            return wallet != null;
        }

        public override bool PerformTax(ICollectableWallet wallet)
        {
            taxedWallet = wallet != null;
            return taxedWallet;
        }
        #endregion
    }

    public class CollectableScanner_Tests: ComponentTestModel<MockCollectableScanner>
    {
        #region Constants
        private CollectableWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.TaxWalletOnScan = true;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(Wallet);

            Wallet = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Check_ReturnsTrueWhenEnabledComponentAndCriteriaFulfilled()
        {
            Assert.True(modelReference.Check(Wallet));
        }

        [Test]
        public void Check_ReturnsFalseWhenComponentDisabled()
        {
            modelReference.enabled = false;

            Assert.False(modelReference.Check(Wallet));
        }

        [Test]
        public void Check_ReturnsFalseWhenCriteriaNotFulfilled()
        {
            Assert.False(modelReference.Check(null));
        }

        [Test]
        public void Check_FiresOnScannedWalletEvent()
        {
            bool scanWasSuccessful = false;
            modelReference.onScanWallet += (result) => scanWasSuccessful = result;
            modelReference.Scan(Wallet);

            Assert.True(scanWasSuccessful);
            modelReference.Scan(null);
            Assert.False(scanWasSuccessful);
        }

        [Test]
        public void Scan_TaxesWalletWhenCheckIsSuccessful()
        {
            modelReference.Scan(Wallet);

            Assert.True(modelReference.taxedWallet);
        }

        [Test]
        public void Scan_KeepsValuesWhenCheckFails()
        {
            modelReference.Scan(null);

            Assert.False(modelReference.taxedWallet);
        }

        [Test]
        public void ScanWallet_WorksTheSameAsScanMethod()
        {
            modelReference.ScanWallet(Wallet);

            Assert.True(modelReference.taxedWallet);
        }

        [Test]
        public void Scan_InvokesOnScanWalletEvent()
        {
            bool scanWasSuccessful = false;
            modelReference.onScanWallet += (result) => scanWasSuccessful = result;
            modelReference.Scan(Wallet);

            Assert.True(scanWasSuccessful);
            modelReference.Scan(null);
            Assert.False(scanWasSuccessful);
        }

        [Test]
        public void Tax_InvokesEventWhenTaxingIsSuccessful()
        {
            bool taxedWallet = false;
            modelReference.onTaxWallet += () => taxedWallet = true;
            modelReference.Tax(Wallet);

            Assert.True(taxedWallet);
        }

        [Test]
        public void Tax_DoesntInvokeEventWhenTaxingFails()
        {
            bool taxedWallet = false;
            modelReference.onTaxWallet += () => taxedWallet = true;
            modelReference.Tax(null);

            Assert.False(taxedWallet);
        }

        [Test]
        public void TaxWallet_WorksTheSameAsTaxMethod()
        {
            int taxCount = 0;
            modelReference.onTaxWallet += () => taxCount++;
            modelReference.Tax(Wallet);

            Assert.AreEqual(1, taxCount);
            modelReference.Tax(null);
            Assert.AreEqual(1, taxCount);
        }
        #endregion
    }
}