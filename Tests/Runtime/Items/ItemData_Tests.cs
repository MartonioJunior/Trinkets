using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemData_Tests: ScrobTestModel<ItemData_Dummy>
    {
        #region Constants
        private const int ItemValue = 500;
        private const string ItemName = "Shield";
        private ItemCategory Category;
        private Sprite Image;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Category);
            EngineScrob.Instance(out Wallet);
            Image = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Name = ItemName;
            modelReference.Image = Image;
            modelReference.Category = Category;
            modelReference.Value = ItemValue;

            modelReference.Setup();
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Category);
            ScriptableObject.DestroyImmediate(Wallet);
            Sprite.DestroyImmediate(Image);

            Category = null;
            Wallet = null;
            Image = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Category_ReturnsCategoryOfItem()
        {
            Assert.AreEqual(Category, modelReference.Category);
        }

        [Test]
        public void Image_ReturnsIconOfItem()
        {
            Assert.AreEqual(Image, modelReference.Image);
        }

        [Test]
        public void FilterName_UsesTheNotationTypeHashtagDisplayName()
        {
            string filterName = $"{modelReference.GetType().Name}#{modelReference.Name}";
            Assert.AreEqual(filterName, modelReference.FilterName);
        }

        [Test]
        public void Name_ReturnsNameOfItem()
        {
            Assert.AreEqual(ItemName, modelReference.Name);
        }

        [Test]
        public void InstanceOn_CreatesNewItemInstanceOnWallet()
        {
            modelReference.InstanceOn(Wallet);
            Assert.AreEqual(1, Wallet.AmountOf(modelReference));
        }

        [Test]
        public void GetInstancesOn_ReturnsAllInstancesOfItemInWallet()
        {
            const int Amount = 3;
            Wallet.InstanceMultiple(modelReference, Amount);
            var result = modelReference.GetInstancesOn(Wallet);

            Assert.AreEqual(Amount, result.Length);
            Assert.AreEqual(ItemName, result[0].Name);
            Assert.AreEqual(ItemName, result[1].Name);
            Assert.AreEqual(ItemName, result[2].Name);
        }
        #endregion
    }
}