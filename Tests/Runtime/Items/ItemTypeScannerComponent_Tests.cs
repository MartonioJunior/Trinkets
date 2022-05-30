using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemTypeScannerComponent_Tests: ComponentTestModel<ItemTypeScannerComponent>
    {
        private const int RequiredAmount = 4;
        #region Constants
        private ItemData_Dummy Item;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Item);
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Item = Item;
            modelReference.Amount = RequiredAmount;
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Item);
            ScriptableObject.DestroyImmediate(Wallet);

            Item = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void FulfillsCriteria_TrueWhenAmountOnWalletGreaterOrEqualToAmount()
        {
            Wallet.InstanceMultiple(Item, RequiredAmount);

            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_FalseWhenAmountSmallerThanRequired()
        {
            Wallet.InstanceMultiple(Item, RequiredAmount-2);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_FalseWhenItemIsNotSet()
        {
            modelReference.Item = null;
            Wallet.InstanceMultiple(Item, 9);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void PerformTax_RemovesAmountOfItemFromWallet()
        {
            const int ItemAmount = 10;
            Wallet.InstanceMultiple(Item, ItemAmount);
            modelReference.PerformTax(Wallet);

            Assert.AreEqual(ItemAmount - RequiredAmount, Wallet.AmountOf(Item));
        }

        [Test]
        public void PerformTax_ReturnsTrueWhenAmountGreaterThanZero()
        {
            Assert.True(modelReference.PerformTax(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsFalseWhenAmountIsZero()
        {
            modelReference.Amount = 0;
            Assert.False(modelReference.PerformTax(Wallet));
        }
        #endregion
    }
}