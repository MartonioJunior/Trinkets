using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemWallet_Tests: ScrobTestModel<ItemWallet>
    {
        #region Constants
        private ItemCategory Category;
        private ItemData_Dummy Flag;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Flag);
            EngineScrob.Instance(out Category);

            base.CreateTestContext();
        }

        public override void ConfigureValues() {}

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Flag);
            ScriptableObject.DestroyImmediate(Category);

            Flag = null;
            Category = null;

            base.CreateTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_ReturnsTrueWhenAdditionIsSuccessful()
        {
            Assert.True(modelReference.Add(Flag));

            var result = modelReference.Search(null);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Flag.FilterName, result[0].FilterName);
        }

        [Test]
        public void Add_ReturnsFalseWhenItemIsNull()
        {
            Assert.False(modelReference.Add(null));
            Assert.Zero(modelReference.Search(null).Length);
        }

        [Test]
        public void AmountOf_Item_ReturnsAmountOfItemOnWallet()
        {
            modelReference.Add(Flag);

            Assert.AreEqual(1, modelReference.AmountOf(Flag));
        }

        [Test]
        public void AmountOf_Item_ReturnsZeroWhenNoItemsOfSameTypeOnWallet()
        {
            Assert.Zero(modelReference.AmountOf(Flag));
        }

        [Test]
        public void AmountOf_ItemCategory_CountsNumberOfItemsFromCategory()
        {
            EngineScrob.Instance(out ItemData_Dummy Batter);
            modelReference.InstanceMultiple(Batter, 5);

            Flag.Category = Category;
            modelReference.InstanceMultiple(Flag, 9);

            Assert.AreEqual(9, modelReference.AmountOf(Category));
            ScriptableObject.DestroyImmediate(Batter);
        }

        [Test]
        public void AmountOf_ItemCategory_ReturnsZeroWhenNoItemsOfCategory()
        {
            modelReference.InstanceMultiple(Flag, 5);
            Assert.Zero(modelReference.AmountOf(Category));
        }

        [Test]
        public void Clear_EmptiesWallet()
        {
            modelReference.InstanceMultiple(Flag, 8);
            modelReference.Clear();

            Assert.Zero(modelReference.Search(null).Length);
        }

        [Test]
        public void InstanceMultiple_InstanceMultipleItemsOnWallet()
        {
            const int ItemCount = 3;
            modelReference.InstanceMultiple(Flag, ItemCount);
            var result = modelReference.Search(null);

            Assert.AreEqual(ItemCount, result.Length);
            Assert.AreEqual(Flag.FilterName, result[0].FilterName);
            Assert.AreEqual(Flag.FilterName, result[1].FilterName);
            Assert.AreEqual(Flag.FilterName, result[2].FilterName);
        }

        [Test]
        public void Remove_Item_ReturnsTrueWhenRemovalIsSuccessful()
        {
            modelReference.InstanceMultiple(Flag, 2);
            Assert.True(modelReference.Remove(Flag));

            var result = modelReference.Search(null);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Flag.FilterName, result[0].FilterName);
        }

        [Test]
        public void Remove_Item_ReturnsFalseWhenItemNotOnWallet()
        {
            modelReference.Add(Flag);
            modelReference.Clear();

            Assert.False(modelReference.Remove(Flag));
        }

        [Test]
        public void Remove_Item_ReturnsFalseWhenKeyNotOnDictionary()
        {
            Assert.False(modelReference.Remove(Flag));
        }

        [Test]
        public void Remove_ItemCategory_RemovesItemsOfCategory()
        {
            EngineScrob.Instance(out ItemData_Dummy Pants);
            modelReference.InstanceMultiple(Pants, 2);
            Flag.Category = Category;
            modelReference.InstanceMultiple(Flag, 7);
            modelReference.Remove(Category, 6);

            var resultA = modelReference.Search((item) => item?.Category?.Name == Category.Name);
            Assert.AreEqual(1, resultA.Length);
            Assert.AreEqual(Category, resultA[0].Category);

            modelReference.Remove(Category, 2);
            resultA = modelReference.Search((item) => item?.Category?.Name == Category.Name);
            var resultB = modelReference.Search(null);
            Assert.Zero(resultA.Length);
            Assert.AreEqual(2, resultB.Length);
            Assert.Null(resultB[0].Category);
            Assert.Null(resultB[1].Category);

            ScriptableObject.DestroyImmediate(Pants);
        }

        [Test]
        public void Remove_ItemCategory_DoesNothingWhenAmountIsZeroOrNegative()
        {
            Flag.Category = Category;
            modelReference.InstanceMultiple(Flag, 1);
            modelReference.Remove(Category, 0);
            modelReference.Remove(Category, -4);
            
            var resultA = modelReference.Search((item) => item?.Category?.Name == Category.Name);
            Assert.AreEqual(1, resultA.Length);
            Assert.AreEqual(Category, resultA[0].Category);
        }

        [Test]
        public void Search_ReturnsItemsWhichFulfillPredicate()
        {
            EngineScrob.Instance(out ItemData_Dummy Shoes);
            modelReference.InstanceMultiple(Shoes, 2);
            Flag.Category = Category;
            modelReference.InstanceMultiple(Flag, 7);

            var result = modelReference.Search((item) => item.Category == null);
            Assert.AreEqual(2, result.Length);
            Assert.Null(result[0].Category);
            Assert.Null(result[1].Category);

            ScriptableObject.DestroyImmediate(Shoes);
        }

        [Test]
        public void Search_ReturnsEmptyArrayOnEmptyWallet()
        {
            Assert.Zero(modelReference.Search(null).Length);
        }

        [Test]
        public void Search_ReturnsAllItemsWhenPredicateIsNull()
        {
            EngineScrob.Instance(out ItemData_Dummy Glove);
            modelReference.InstanceMultiple(Glove, 1);
            Flag.Category = Category;
            modelReference.InstanceMultiple(Flag, 1);

            var result = modelReference.Search(null);
            Assert.AreEqual(2, result.Length);
            Assert.Null(result[0].Category);
            Assert.AreEqual(Category, result[1].Category);
        }
        #endregion
    }
}