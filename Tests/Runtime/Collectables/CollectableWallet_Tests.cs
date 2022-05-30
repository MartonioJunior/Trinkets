using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Collectables;
using MartonioJunior.Collectables;
using System;

namespace Tests.MartonioJunior.Collectables.Collectables
{
    public class CollectableWallet_Tests: ScrobTestModel<CollectableWallet>
    {
        #region Constants
        private CollectableCategory EmptyCategory;
        private CollectableCategory Category;
        private CollectableData StartCollectable;
        private CollectableData NewCollectableA;
        private CollectableData NewCollectableB;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out StartCollectable);
            EngineScrob.Instance(out NewCollectableA);
            EngineScrob.Instance(out NewCollectableB);

            EngineScrob.Instance(out Category);
            NewCollectableA.Category = Category;
            NewCollectableB.Category = Category;

            EngineScrob.Instance(out EmptyCategory);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Add(StartCollectable);
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(EmptyCategory);
            ScriptableObject.DestroyImmediate(Category);
            ScriptableObject.DestroyImmediate(StartCollectable);
            ScriptableObject.DestroyImmediate(NewCollectableA);
            ScriptableObject.DestroyImmediate(NewCollectableB);

            EmptyCategory = null;
            Category = null;
            StartCollectable = null;
            NewCollectableA = null;
            NewCollectableB = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_ICollectable_ReturnsTrueWhenCollectableNotInWallet()
        {
            Assert.True(modelReference.Add(NewCollectableA));
        }

        [Test]
        public void Add_ICollectable_ReturnsFalseWhenCollectableIsAlreadyInWallet()
        {
            Assert.False(modelReference.Add(StartCollectable));
        }

        [Test]
        public void Add_ICollectableCategory_ReturnsTrueWhenAnyCollectableInCategoryIsAdded()
        {
            Assert.True(modelReference.Add(Category));
        }

        [Test]
        public void Add_ICollectableCategory_ReturnsFalseWhenAllCollectablesAlreadyInWallet()
        {
            modelReference.Add(NewCollectableA);
            modelReference.Add(NewCollectableB);

            Assert.False(modelReference.Add(Category));
        }

        [Test]
        public void Add_ICollectableCategory_ReturnsFalseWhenCategoryHasNoCollectables()
        {
            Assert.False(modelReference.Add(EmptyCategory));
        }

        [Test]
        public void Add_AmountOfICollectableCategory_InsertsNElementsOfCategoryIntoWallet()
        {
            modelReference.Add(Category, 1);

            Assert.AreNotEqual(modelReference.Contains(NewCollectableA), modelReference.Contains(NewCollectableB));
        }

        [Test]
        public void Add_AmountOfICollectableCategory_InsertsNothingWhenAmountIsNegativeOrZero()
        {
            modelReference.Add(Category, 0);

            Assert.False(modelReference.Contains(NewCollectableA));
            Assert.False(modelReference.Contains(NewCollectableB));
        }

        [Test]
        public void AmountOf_ICollectable_ReturnsOneWhenCollectableInWallet()
        {
            Assert.AreEqual(1, modelReference.AmountOf(StartCollectable));
        }

        [Test]
        public void AmountOf_ICollectable_ReturnsZeroWhenCollectableNotInWallet()
        {
            Assert.Zero(modelReference.AmountOf(NewCollectableB));
        }

        [Test]
        public void AmountOf_ICollectableCategory_ReturnsElementCountOnWalletBelongingToCategory()
        {
            modelReference.Add(NewCollectableB);

            Assert.AreEqual(1, modelReference.AmountOf(Category));
        }

        [Test]
        public void AmountOf_ICollectableCategory_ReturnsZeroWhenCategoryIsNotOnWallet()
        {
            Assert.Zero(modelReference.AmountOf(EmptyCategory));
        }

        [Test]
        public void Clear_RemovesAllContentsOfWallet()
        {
            modelReference.Clear();

            Assert.False(modelReference.Contains(StartCollectable));
        }

        [Test]
        public void Search_ICollectable_ReturnsListOfCollectablesWhichFulfillPredicate()
        {
            modelReference.Add(NewCollectableA);
            Predicate<ICollectable> predicate = (item) => Category.Contains(item);
            var result = modelReference.Search(predicate);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(NewCollectableA, result[0]);
        }

        [Test]
        public void Search_ICollectable_ReturnsEmptyArrayWhenNoneFulfillPredicate()
        {
            modelReference.Add(Category);
            Predicate<ICollectable> predicate = (item) => EmptyCategory.Contains(item);
            var result = modelReference.Search(predicate);

            Assert.Zero(result.Length);
        }

        [Test]
        public void Search_ICollectable_ReturnsAllCollectablesWhenPredicateIsNull()
        {
            modelReference.Add(Category);
            Predicate<ICollectable> predicate = null;
            var result = modelReference.Search(predicate);

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(StartCollectable, result[0]);
            Assert.AreEqual(NewCollectableA, result[1]);
            Assert.AreEqual(NewCollectableB, result[2]);
        }

        [Test]
        public void Search_ICollectableCategory_ReturnsListOfCategoriesWhichFulfillPredicate()
        {
            modelReference.Add(NewCollectableA);

            Predicate<ICollectableCategory> predicate = (category) => category.AmountOf(NewCollectableA) > 0;
            var result = modelReference.Search(predicate);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Category, result[0]);
        }

        [Test]
        public void Search_ICollectableCategory_ReturnsEmptyArrayWhenNoneFulfillPredicate()
        {
            Predicate<ICollectableCategory> predicate = (category) => category.Equals(EmptyCategory);
            var result = modelReference.Search(predicate);

            Assert.Zero(result.Length);
        }

        [Test]
        public void Search_ICollectableCategory_ReturnsAllCategoriesWithCollectablesInWalletWhenPredicateIsNull()
        {
            modelReference.Add(NewCollectableB);

            Predicate<ICollectableCategory> predicate = null;
            var result = modelReference.Search(predicate);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Category, result[0]);
        }

        [Test]
        public void Remove_ICollectable_ReturnsTrueWhenCollectableIsRemovedFromWallet()
        {
            Assert.True(modelReference.Remove(StartCollectable));
        }

        [Test]
        public void Remove_ICollectable_ReturnsFalseWhenCollectableNotOnWallet()
        {
            Assert.False(modelReference.Remove(NewCollectableB));
        }

        [Test]
        public void Remove_ICollectable_RemovesFalseWhenCategoryOfCollectableNotOnWallet()
        {
            Assert.False(modelReference.Remove(NewCollectableA));
        }

        [Test]
        public void Remove_ICollectableCategory_ReturnsTrueWhenCategoryInWallet()
        {
            modelReference.Add(NewCollectableA);

            Assert.True(modelReference.Remove(Category));
        }

        [Test]
        public void Remove_ICollectableCategory_ReturnsFalseWhenCategoryNotOnWallet()
        {
            Assert.False(modelReference.Remove(EmptyCategory));
        }

        [Test]
        public void Remove_AmountOfICollectableCategory_RemovesNElementsOfCategoryFromWallet()
        {
            modelReference.Add(Category);
            modelReference.Remove(Category, 1);

            Assert.AreEqual(1, modelReference.AmountOf(Category));
        }

        [Test]
        public void Remove_AmountOfICollectableCategory_DoesNothingWhenCategoryNotOnWallet()
        {
            modelReference.Remove(EmptyCategory, 5);

            Assert.True(modelReference.Contains(StartCollectable));
        }

        [Test]
        public void Remove_AmountOfICollectableCategory_ClearsCategoryFromWalletWhenAmountEqualOrBigger()
        {
            modelReference.Add(NewCollectableA);
            modelReference.Remove(Category, 5);

            Assert.Zero(modelReference.AmountOf(Category));
        }

        [Test]
        public void Contains_ReturnsTrueWhenCollectableOnWallet()
        {
            Assert.True(modelReference.Contains(StartCollectable));
        }

        [Test]
        public void Contains_ReturnsFalseWhenCollectableNotOnWallet()
        {
            Assert.False(modelReference.Contains(NewCollectableA));
        }
        #endregion
    }
}