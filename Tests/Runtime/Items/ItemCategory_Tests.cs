using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemCategory_Tests: ScrobTestModel<ItemCategory>
    {
        #region Constants
        private const string CategoryName = "Armor";
        private Sprite CategoryIcon;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            CategoryIcon = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Name = CategoryName;
            modelReference.Image = CategoryIcon;
        }

        public override void DestroyTestContext()
        {
            Sprite.DestroyImmediate(CategoryIcon);

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Name_ReturnsCategoryName()
        {
            Assert.AreEqual(CategoryName, modelReference.Name);
        }

        [Test]
        public void Name_ChangesToDefaultWhenSetToNullOrEmpty()
        {
            modelReference.Name = null;

            Assert.AreEqual(ItemCategory.DefaultDisplayName, modelReference.Name);
        }

        [Test]
        public void Image_ReturnsSpriteForCategory()
        {
            Assert.AreEqual(CategoryIcon, modelReference.Image);
        }
        #endregion
    }
}