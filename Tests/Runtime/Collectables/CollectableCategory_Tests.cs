using System.Collections;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableCategory_Tests: ScrobTestModel<CollectableCategory>
    {
        #region Constants
        private Sprite Sprite;
        #endregion
        #region ScrobTestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.Image = Value(Mock.Sprite(), out Sprite);
        }
        #endregion
        #region Method Tests
        [Test]
        public void Image_ReturnsSpriteForCategory()
        {
            Assert.AreEqual(Sprite, modelReference.Image);
        }

        [TestCase("Diamonds", "Diamonds")]
        [TestCase(null, CollectableCategory.DefaultCategoryName)]
        public void Name_ReturnsCategoryNameForDisplay(string input, string output)
        {
            modelReference.Name = input;

            Assert.AreEqual(output, modelReference.Name);
        }

        [TestCase("Lollipops", "Lollipops (Collectable Category)")]
        [TestCase(null, CollectableCategory.DefaultCategoryName+" (Collectable Category)")]
        public void ToString_ReturnsCategoryName(string input, string output)
        {
            modelReference.Name = input;

            Assert.AreEqual(output, modelReference.ToString());
        }

        [Test]
        public void Quantifiable_IsTrueForCollectableCategories()
        {
            Assert.True(modelReference.Quantifiable);
        }

        [Test]
        public void Value_AlwaysReturnsOne([Random(-10000,10000,1)] int value)
        {
            modelReference.Value = value;

            Assert.AreEqual(1, modelReference.Value);
        }
        #endregion
    }
}