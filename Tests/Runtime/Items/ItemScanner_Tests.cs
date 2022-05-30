using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;
using Dummy = Tests.MartonioJunior.Collectables.Items.ItemScanner_Dummy;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemScanner_Tests: ComponentTestModel<ItemScanner_Dummy>
    {
        #region Constants
        private ItemData_Dummy Fish;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Fish);
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.TaxWalletOnScan = true;
            
            Wallet.Add(Fish);
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Fish);
            ScriptableObject.DestroyImmediate(Wallet);

            Fish = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Check_ReturnsTrueWhenEnabledAndCriteriaFulfilled()
        {
            Assert.True(modelReference.Check(Wallet));
        }

        [Test]
        public void Check_ReturnsFalseWhenCriteriaNotFulfilled()
        {
            Assert.False(modelReference.Check(null));
        }

        [Test]
        public void Check_ReturnsFalseWhenComponentIsDisabled()
        {
            modelReference.enabled = false;

            Assert.False(modelReference.Check(Wallet));
        }

        [Test]
        public void Tax_PerformsTaxOnWallet()
        {
            modelReference.Tax(Wallet);
            var result = Wallet.Search(null);

            Assert.Zero(result.Length);
        }

        [Test]
        public void Tax_InvokesEventWhenTaxIsSuccessful()
        {
            bool triggeredEvent = false;
            modelReference.onTaxWallet += () => triggeredEvent = true;
            modelReference.Tax(null);
            Assert.False(triggeredEvent);

            modelReference.Tax(Wallet);    
            Assert.True(triggeredEvent);
        }

        [Test]
        public void Scan_InvokesOnScanWalletEvent()
        {
            bool triggeredEvent = true;
            modelReference.onScanWallet += (scanResult) => triggeredEvent = scanResult;
            modelReference.Scan(null);
            Assert.False(triggeredEvent);

            modelReference.Scan(Wallet);
            Assert.True(triggeredEvent);
        }

        [Test]
        public void ScanWallet_WorksTheSameAsScanMethod()
        {
            modelReference.ScanWallet(Wallet);

            Assert.Zero(Wallet.Search(null).Length);
        }

        [Test]
        public void TaxWallet_WorksTheSameAsTaxMethod()
        {
            modelReference.TaxWallet(Wallet);

            Assert.Zero(Wallet.Search(null).Length);
        }
        #endregion
    }
}