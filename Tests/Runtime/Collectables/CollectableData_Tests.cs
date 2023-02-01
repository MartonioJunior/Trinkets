using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Collectables;
using MartonioJunior.Trinkets;
using NSubstitute;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableData_Tests: ScrobTestModel<CollectableData>
    {
        #region Constants
        private const string CollectableName = "Unique Item";
        private const string CategoryName = "Food";
        private Sprite CollectableIcon;
        #endregion
        #region ScrobTestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Category()
        {
            yield return new object[]{ Mock.Category("Empty"), false };
            yield return new object[]{ Substitute<ICollectableCategory>(), true };
            yield return new object[]{ null, false };
        }
        [TestCaseSource(nameof(UseCases_Category))]
        public void Category_ReturnsCategoryOfCollectable(ICollectableCategory input, bool shouldFail)
        {
            modelReference.Category = input;

            var category = modelReference.Category;

            if (shouldFail) {
                Assert.That(category, Is.Null);
            } else {
                Assert.That(category, Is.EqualTo(input));
            }
        }

        [Test]
        public void Collect_InsertsCollectableIntoWallet()
        {
            var wallet = Mock.CollectableWallet;

            modelReference.Collect(wallet);

            Assert.True(wallet.Contains(modelReference));
        }

        [Test]
        public void Image_ReturnsIconForDisplayImage([Values] bool collectableHasImage, [Values] bool categoryHasImage)
        {
            var spriteImage = Mock.Sprite();
            var categoryImage = Mock.Sprite();

            var category = Mock.Category("Test");
            category.Image = null;

            modelReference.Category = category;

            if (categoryHasImage) category.Image = categoryImage;
            if (collectableHasImage) modelReference.Image = spriteImage;

            if (collectableHasImage) {
                Assert.AreEqual(spriteImage, modelReference.Image);
            } else if (categoryHasImage) {
                Assert.AreEqual(categoryImage, modelReference.Image);
            } else {
                Assert.Null(modelReference.Image);
            }
        }

        [TestCase("Tunic", "t_203", "Clothes", "Tunic (Clothes)")]
        [TestCase("", "s_849", "Dress", "s_849 (Dress)")]
        [TestCase("", "k_189", "", "k_189 ("+CollectableData.EmptyCategory+")")]
        [TestCase(null, "", "Equipment", " (Equipment)")]
        [TestCase(null, null, "", " ("+CollectableData.EmptyCategory+")")]
        [TestCase(null, null, null, " ("+CollectableData.EmptyCategory+")")]
        public void Name_ReturnsDisplayNameOfCollectable(string collectableName, string objectName, string categoryName, string output)
        {
            modelReference.Name = collectableName;
            modelReference.name = objectName;
            if (!string.IsNullOrEmpty(categoryName))
                modelReference.Category = Mock.Category(categoryName);

            Assert.AreEqual(output, modelReference.Name);
        }

        [Test]
        public void Quantifiable_IsFalseForCollectables()
        {
            Assert.False(modelReference.Quantifiable);
        }

        [TestCase("f_934", "Item", "f_934(Item)")]
        [TestCase("", "Potion", "(Potion)")]
        [TestCase("key_94", "", "key_94()")]
        [TestCase(null, "", "()")]
        public void ToString_ReturnsCollectableNameAndCategory(string objectName, string categoryName, string output)
        {
            modelReference.name = objectName;
            if (!string.IsNullOrEmpty(categoryName)) modelReference.Category = Mock.Category(categoryName);

            Assert.AreEqual(output, modelReference.ToString());
        }

        [Test]
        public void Value_AlwaysHasOneAsValue([Random(-10000,10000,1)] int input)
        {
            modelReference.Value = input;

            Assert.AreEqual(1, modelReference.Value);
        }

        [Test]
        public void WasCollectedBy_ChecksWhenCollectableIsInSuppliedWallet([Values] bool inWallet)
        {
            var wallet = Mock.CollectableWallet;
            if (inWallet) wallet.Add((ResourceData)modelReference);

            Assert.AreEqual(inWallet, modelReference.WasCollectedBy(wallet));
        }

        [Test]
        public void Cast_ResourceData_ReturnsValueForResource()
        {
            var result = (ResourceData)modelReference;

            Assert.AreEqual(modelReference, result.Resource);
            Assert.AreEqual(1, result.Amount);
        }
        #endregion
    }
}