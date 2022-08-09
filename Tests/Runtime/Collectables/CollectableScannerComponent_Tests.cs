using System.Collections;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableScannerComponent_Tests: ComponentTestModel<CollectableScannerComponent>
    {
        #region Constants
        private CollectableData CollectableA;
        private CollectableData CollectableB;
        private CollectableData CollectableC;
        private CollectableWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out CollectableA);
            EngineScrob.Instance(out CollectableB);
            EngineScrob.Instance(out CollectableC);
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.SetCriteria(CollectableB, CollectableC);
            modelReference.TaxWalletOnScan = true;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(CollectableA);
            ScriptableObject.DestroyImmediate(CollectableB);
            ScriptableObject.DestroyImmediate(CollectableC);
            ScriptableObject.DestroyImmediate(Wallet);

            CollectableA = null;
            CollectableB = null;
            CollectableC = null;
            Wallet = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void FulfillsCriteria_ReturnsTrueWhenAllCollectablesInWallet()
        {
            Wallet.Add(CollectableB);
            Wallet.Add(CollectableC);

            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_ReturnsTrueWhenCollectableListIsEmpty()
        {
            modelReference.SetCriteria(null);

            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_ReturnsFalseForAnyCollectableNotInWallet()
        {
            Wallet.Add(CollectableA);
            Wallet.Add(CollectableB);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsTrueWhenRemovalIsSuccessful()
        {
            Wallet.Add(CollectableB);
            Wallet.Add(CollectableC);
            var successfulTax = modelReference.PerformTax(Wallet);

            Assert.True(successfulTax);
            Assert.False(Wallet.Contains(CollectableB));
            Assert.False(Wallet.Contains(CollectableC));
        }

        [Test]
        public void PerformTax_ReturnsFalseWhenCollectablesListIsNullOrEmpty()
        {
            modelReference.SetCriteria(null);
            var successfulTax = modelReference.PerformTax(Wallet);

            Assert.False(successfulTax);
        }

        [Test]
        public void SetCriteria_DefinesCollectablesUsedByComponent()
        {
            modelReference.SetCriteria(CollectableA);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
            Wallet.Add(CollectableA);
            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void SetCriteria_ClearsCriteriaWhenArrayNullOrEmpty()
        {
            modelReference.SetCriteria(null);

            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }
        #endregion
    }
}