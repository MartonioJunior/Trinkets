using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class Item_Tests: TestModel<Item_Dummy>
    {
        #region Constants
        private const int ItemValue = 500;
        private const string ItemName = "Shield";
        private ItemModel_Dummy Model;
        private ItemCategory Category;
        private Sprite Image;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Category);
            Image = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);

            EngineScrob.Instance(out Model);
            Model.Category = Category;
            Model.Name = ItemName;

            modelReference = Model.New;
            modelReference.Image = Image;
            modelReference.Value = ItemValue;
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Category);
            Sprite.DestroyImmediate(Image);

            Category = null;
            Image = null;

            modelReference = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Model_ReturnsObjectThatCreatedItem()
        {
            Assert.AreEqual(Model, modelReference.Model);
        }

        [Test]
        public void Image_ReturnsIconOfItem()
        {
            Assert.AreEqual(Image, modelReference.Image);
        }

        [Test]
        public void Name_ReturnsNameOfItem()
        {
            Assert.AreEqual(ItemName, modelReference.Name);
        }

        [Test]
        public void Value_ReturnsItemWorth()
        {
            Assert.AreEqual(ItemValue, modelReference.Value);
        }

        [Test]
        public void Copy_CreatesNewItemInstance()
        {
            const int CopyValue = ItemValue + 1;
            var newItem = modelReference.Copy();

            Assert.AreEqual(CopyValue, newItem.Value);
            Assert.AreEqual(ItemName, newItem.Name);
            Assert.AreEqual(Image, newItem.Image);
            Assert.AreEqual(Model, newItem.Model);
        }
        #endregion
    }
}