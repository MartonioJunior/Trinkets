using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemWallet_Tests: ScrobTestModel<ItemWallet>
    {
        #region Constants
        private ItemCategory Category;
        private ItemModel_Dummy FlagModel;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out FlagModel);
            EngineScrob.Instance(out Category);

            base.CreateTestContext();
        }

        public override void ConfigureValues() {}

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(FlagModel);
            ScriptableObject.DestroyImmediate(Category);

            FlagModel = null;
            Category = null;

            base.CreateTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_ReturnsTrueWhenAdditionIsSuccessful()
        {
            var Flag = FlagModel.New;
            Assert.True(modelReference.Add(Flag));

            var result = modelReference.Search(null);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(Flag, result[0]);
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
            var Flag = FlagModel.New;
            modelReference.Add(Flag);

            Assert.AreEqual(1, modelReference.AmountOf(Flag));
        }

        [Test]
        public void AmountOf_Item_ReturnsZeroWhenNoItemsOfSameTypeOnWallet()
        {
            var Flag = FlagModel.New;
            Assert.Zero(modelReference.AmountOf(Flag));
        }

        [Test]
        public void AmountOf_ItemModel_ReturnsAmountOfItemOnWallet()
        {
            var Flag = FlagModel.New;
            modelReference.Add(Flag);

            Assert.AreEqual(1, modelReference.AmountOf(FlagModel));
        }

        [Test]
        public void AmountOf_ItemModel_ReturnsZeroWhenNoItemsOfSameTypeOnWallet()
        {
            Assert.Zero(modelReference.AmountOf(FlagModel));
        }

        [Test]
        public void AmountOf_ItemCategory_CountsNumberOfItemsFromCategory()
        {
            EngineScrob.Instance(out ItemModel_Dummy Batter);
            modelReference.InstanceMultiple(Batter, 5);

            FlagModel.Category = Category;
            modelReference.InstanceMultiple(FlagModel, 9);

            Assert.AreEqual(9, modelReference.AmountOf(Category));
            ScriptableObject.DestroyImmediate(Batter);
        }

        [Test]
        public void AmountOf_ItemCategory_ReturnsZeroWhenNoItemsOfCategory()
        {
            modelReference.InstanceMultiple(FlagModel, 5);
            Assert.Zero(modelReference.AmountOf(Category));
        }

        [Test]
        public void Clear_EmptiesWallet()
        {
            modelReference.InstanceMultiple(FlagModel, 8);
            modelReference.Clear();

            Assert.Zero(modelReference.Search(null).Length);
        }

        [Test]
        public void CopyMultiple_InstancesMultipleItemsWithSameValuesOnWallet()
        {
            const int ItemCount = 3;
            const int ItemValue = 2;
            var Flag = FlagModel.New;
            modelReference.CopyMultiple(Flag, ItemCount);
            var result = modelReference.Search(null);

            Assert.AreEqual(ItemCount, result.Length);
            Assert.AreEqual(ItemValue, result[0].Value);
            Assert.AreEqual(ItemValue, result[1].Value);
            Assert.AreEqual(ItemValue, result[2].Value);
        }

        [Test]
        public void InstanceMultiple_CreatesMultipleItemsFromModelOnWallet()
        {
            const int ItemCount = 3;
            modelReference.InstanceMultiple(FlagModel, ItemCount);
            var result = modelReference.Search(null);

            Assert.AreEqual(ItemCount, result.Length);
            Assert.AreEqual(FlagModel, result[0].Model);
            Assert.AreEqual(FlagModel, result[1].Model);
            Assert.AreEqual(FlagModel, result[2].Model);
        }

        [Test]
        public void Remove_Item_ReturnsTrueWhenRemovalIsSuccessful()
        {
            var Flag = FlagModel.New;
            modelReference.InstanceMultiple(FlagModel, 1);
            modelReference.Add(Flag);
            Assert.True(modelReference.Remove(Flag));

            var result = modelReference.Search(null);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(FlagModel, result[0].Model);
        }

        [Test]
        public void Remove_Item_ReturnsFalseWhenItemNotOnWallet()
        {
            var Flag = FlagModel.New;
            modelReference.Add(Flag);
            modelReference.Clear();

            Assert.False(modelReference.Remove(Flag));
        }

        [Test]
        public void Remove_Item_ReturnsFalseWhenKeyNotOnDictionary()
        {
            Assert.False(modelReference.Remove(FlagModel.New));
        }

        [Test]
        public void Remove_ItemCategory_RemovesItemsOfCategory()
        {
            EngineScrob.Instance(out ItemModel_Dummy Pants);
            modelReference.InstanceMultiple(Pants, 2);
            FlagModel.Category = Category;
            modelReference.InstanceMultiple(FlagModel, 7);
            modelReference.Remove(Category, 6);

            var resultA = modelReference.Search((item) => Category.Equals(item.Model.Category));
            Assert.AreEqual(1, resultA.Length);
            Assert.True(Category.Equals(resultA[0].Model.Category));

            modelReference.Remove(Category, 2);
            resultA = modelReference.Search((item) => Category.Equals(item.Model.Category));
            var resultB = modelReference.Search(null);
            Assert.Zero(resultA.Length);
            Assert.AreEqual(2, resultB.Length);
            Assert.Null(resultB[0].Model.Category);
            Assert.Null(resultB[1].Model.Category);

            ScriptableObject.DestroyImmediate(Pants);
        }

        [Test]
        public void Remove_ItemCategory_DoesNothingWhenAmountIsZeroOrNegative()
        {
            FlagModel.Category = Category;
            modelReference.InstanceMultiple(FlagModel, 1);
            modelReference.Remove(Category, 0);
            modelReference.Remove(Category, -4);
            
            var resultA = modelReference.Search((item) => Category.Equals(item.Model.Category));
            Assert.AreEqual(1, resultA.Length);
            Assert.True(Category.Equals(resultA[0].Model.Category));
        }

        [Test]
        public void Search_ReturnsItemsWhichFulfillPredicate()
        {
            EngineScrob.Instance(out ItemModel_Dummy Shoes);
            modelReference.InstanceMultiple(Shoes, 2);
            FlagModel.Category = Category;
            modelReference.InstanceMultiple(FlagModel, 7);

            var result = modelReference.Search((item) => item.Model.Category == null);
            Assert.AreEqual(2, result.Length);
            Assert.Null(result[0].Model.Category);
            Assert.Null(result[1].Model.Category);

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
            EngineScrob.Instance(out ItemModel_Dummy Glove);
            modelReference.InstanceMultiple(Glove, 1);
            FlagModel.Category = Category;
            modelReference.InstanceMultiple(FlagModel, 1);

            var result = modelReference.Search(null);
            Assert.AreEqual(2, result.Length);
            Assert.Null(result[0].Model.Category);
            Assert.True(Category.Equals(result[1].Model.Category));
        }
        #endregion
    }
}