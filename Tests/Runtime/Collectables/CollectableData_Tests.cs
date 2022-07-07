using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Collectables;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableData_Tests: ScrobTestModel<CollectableData>
    {
        #region Constants
        private CollectableCategory Category;
        private CollectableWallet Wallet;
        private Sprite CategoryIcon;
        #endregion
        #region ScrobTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Category);
            EngineScrob.Instance(out Wallet);

            CategoryIcon = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);
            Category.Image = CategoryIcon;
            
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Category = Category;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(Category);
            ScriptableObject.DestroyImmediate(Wallet);
            Sprite.DestroyImmediate(CategoryIcon);

            Category = null;
            Wallet = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Category_ReturnsCategoryOfCollectable()
        {
            Assert.AreEqual(Category, modelReference.Category);
        }

        [Test]
        public void Category_SetReplacesCategory()
        {
            EngineScrob.Instance(out CollectableCategory OtherCategory);
            modelReference.Category = OtherCategory;

            Assert.False(Category.Contains(modelReference));
            Assert.True(OtherCategory.Contains(modelReference));
            ScriptableObject.DestroyImmediate(OtherCategory);
        }

        [Test]
        public void Collect_InsertsCollectableIntoWallet()
        {
            modelReference.Collect(Wallet);

            Assert.True(Wallet.Contains(modelReference));
        }

        [Test]
        public void Image_ReturnsIconForCategoryOfCollectable()
        {
            Assert.AreEqual(CategoryIcon, modelReference.Image);
        }

        [Test]
        public void Image_ReturnsNullWhenCategoryNotSet()
        {
            modelReference.Category = null;
            Assert.Null(modelReference.Category);
        }

        [Test]
        public void Name_ReturnsNameOfCollectableCategory()
        {
            Assert.AreEqual(Category.Name, modelReference.Name);
        }

        [Test]
        public void Name_CannotBeAlteredByDefault()
        {
            const string NewName = "Lollipop";
            modelReference.Name = NewName;

            Assert.AreNotEqual(NewName, modelReference.Name);
        }

        [Test]
        public void Setup_AddsCategoryIfSet()
        {
            Category.Remove(modelReference);
            modelReference.Setup();

            Assert.True(Category.Contains(modelReference));
        }

        [Test]
        public void TearDown_RemovesCategoryIfSet()
        {
            modelReference.TearDown();

            Assert.False(Category.Contains(modelReference));
        }

        [Test]
        public void Value_HasOneAsTheDefaultValue()
        {
            Assert.AreEqual(1, modelReference.Value);
        }

        [Test]
        public void Value_CannotBeAlteredByDefault()
        {
            const int NewValue = 6;
            modelReference.Value = NewValue;

            Assert.AreNotEqual(NewValue, modelReference.Value);
        }

        [Test]
        public void WasCollectedBy_ReturnsTrueWhenCollectableInWallet()
        {
            Wallet.Add(modelReference);

            Assert.True(modelReference.WasCollectedBy(Wallet));
        }

        [Test]
        public void WasCollectedBy_ReturnsFalseWhenCollectableNotOnWallet()
        {
            Assert.False(modelReference.WasCollectedBy(Wallet));
        }
        #endregion
    }
}