using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemCategoryScannerComponent_Tests: ComponentTestModel<ItemCategoryScannerComponent>
    {
        #region Constants
        private const int RequiredAmount = 5;
        private ItemWallet Wallet;
        private ItemCategory Category;
        private ItemModel_Dummy Weapon;
        private ItemModel_Dummy Potion;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Weapon);
            EngineScrob.Instance(out Potion);
            EngineScrob.Instance(out Category);
            EngineScrob.Instance(out Wallet);
            Weapon.Category = Category;

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Amount = RequiredAmount;
            modelReference.Category = Category;
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Weapon);
            ScriptableObject.DestroyImmediate(Potion);
            ScriptableObject.DestroyImmediate(Category);
            ScriptableObject.DestroyImmediate(Wallet);

            Weapon = null;
            Potion = null;
            Category = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Amount_ReturnsQuantityRequired()
        {
            Assert.AreEqual(RequiredAmount, modelReference.Amount);
        }

        [Test]
        public void FulfillsCriteria_TrueWhenAmountOfCategoryOnWalletIsGreaterOrEqual()
        {
            Wallet.InstanceMultiple(Weapon, 6);
            Assert.True(modelReference.FulfillsCriteria(Wallet));

            Wallet.InstanceMultiple(Weapon, 3);
            Assert.True(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_FalseWhenAmountOfCategoryOnWalletIsLower()
        {
            Wallet.InstanceMultiple(Weapon, 3);
            Wallet.InstanceMultiple(Potion, 12);

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void FulfillsCriteria_FalseWhenCategoryIsNotSet()
        {
            Wallet.InstanceMultiple(Weapon, 9);
            Wallet.InstanceMultiple(Potion, 6);
            modelReference.Category = null;

            Assert.False(modelReference.FulfillsCriteria(Wallet));
        }

        [Test]
        public void PerformTax_ReturnsTrueWhenAmountGreaterThanZero()
        {
            const int WeaponCount = 13;
            Wallet.InstanceMultiple(Weapon, WeaponCount);

            Assert.True(modelReference.PerformTax(Wallet));
            Assert.AreEqual(WeaponCount - RequiredAmount, Wallet.AmountOf(Weapon));
        }

        [Test]
        public void PerformTax_ReturnsFalseWhenAmountIsZeroOrLower()
        {
            const int PotionCount = 7;
            Wallet.InstanceMultiple(Potion, PotionCount);
            modelReference.Amount = 0;
            Assert.False(modelReference.PerformTax(Wallet));

            modelReference.Amount = -42;
            Assert.False(modelReference.PerformTax(Wallet));
            Assert.AreEqual(PotionCount, Wallet.AmountOf(Potion));
        }
        #endregion
    }
}