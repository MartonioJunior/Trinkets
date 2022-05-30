using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Collectables;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Collectables
{
    public class CollectableCategoryScannerComponent_Tests: ComponentTestModel<CollectableCategoryScannerComponent>
    {
        #region Constants
        private const int AmountScan = 1;
        private CollectableWallet Wallet;
        private CollectableCategory Category;
        private CollectableData InternalCollectable;
        private CollectableData ExternalCollectable;
        #endregion
        #region ComponentTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Category);
            EngineScrob.Instance(out Wallet);
            EngineScrob.Instance(out InternalCollectable);
            EngineScrob.Instance(out ExternalCollectable);

            base.CreateTestContext();

            InternalCollectable.Category = Category;
        }
        public override void ConfigureValues()
        {
            modelReference.Category = Category;
            modelReference.Amount = AmountScan;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(Category);
            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(InternalCollectable);
            ScriptableObject.DestroyImmediate(ExternalCollectable);

            Category = null;
            Wallet = null;
            InternalCollectable = null;
            ExternalCollectable = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void FulfillsCriteria_ReturnsTrueWhenWalletHasAmountOfResource()
        {
            Wallet.Add(InternalCollectable);

            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_ReturnsFalseWhenWalletHasNotEnoughResource()
        {
            Wallet.Clear();
            Wallet.Add(ExternalCollectable);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_ReturnsFalseWhenCategoryIsEmpty()
        {
            modelReference.Category = null;

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsTrueWhenAmountIsPositiveAndCategoryHasValue()
        {
            Assert.True(modelReference.PerformTax(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsFalseWhenAmountIsLowerThanOne()
        {
            modelReference.Amount = 0;

            Assert.False(modelReference.PerformTax(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsFalseWhenCategoryIsNotSet()
        {
            modelReference.Category = null;

            Assert.False(modelReference.PerformTax(Wallet));
        }
        #endregion
    }
}