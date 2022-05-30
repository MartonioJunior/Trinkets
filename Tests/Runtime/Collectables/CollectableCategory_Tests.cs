using System.Collections;
using MartonioJunior.Collectables;
using MartonioJunior.Collectables.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Collectables.Collectables
{
    public class CollectableCategory_Tests: ScrobTestModel<CollectableCategory>
    {
        #region Constants
        public const string StartName = "Gems";
        private CollectableData StartCollectable;
        private CollectableData NewCollectable;
        #endregion
        #region ScrobTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out StartCollectable);
            EngineScrob.Instance(out NewCollectable);
            
            base.CreateTestContext();

            modelReference.Add(StartCollectable);
        }

        public override void ConfigureValues()
        {
            modelReference.Name = StartName;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(StartCollectable);
            ScriptableObject.DestroyImmediate(NewCollectable);

            StartCollectable = null;
            NewCollectable = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_ReturnsTrueWhenCollectableAddedSuccessfully()
        {
            Assert.True(modelReference.Add(NewCollectable));
            Assert.True(modelReference.Contains(NewCollectable));
        }

        [Test]
        public void Add_ReturnsFalseWhenCollectableAlreadyPartOfCategory()
        {
            Assert.False(modelReference.Add(StartCollectable));
        }

        [Test]
        public void AmountOf_ReturnsOneWhenCollectableIsPartOfCategory()
        {
            Assert.AreEqual(1, modelReference.AmountOf(StartCollectable));
        }

        [Test]
        public void AmountOf_ReturnsZeroWhenCollectableNotInCategory()
        {
            Assert.Zero(modelReference.AmountOf(NewCollectable));
        }

        [Test]
        public void Clear_RemovesAllElementsOfCategory()
        {
            modelReference.Clear();

            Assert.Zero(modelReference.Value);
            Assert.Zero(modelReference.Search(null).Length);
        }

        [Test]
        public void Contains_ReturnsTrueWhenCollectablePartOfCategory()
        {
            Assert.True(modelReference.Contains(StartCollectable));
        }

        [Test]
        public void Contains_ReturnsFalseWhenCollectableNotInCategory()
        {
            modelReference.Clear();

            Assert.False(modelReference.Contains(StartCollectable));
        }

        [Test]
        public void Name_ReturnsCategoryNameForDisplay()
        {
            Assert.AreEqual(StartName, modelReference.Name);
        }

        [Test]
        public void Name_ReturnsDefaultDisplayNameWhenNull()
        {
            modelReference.Name = null;

            Assert.AreEqual(CollectableCategory.DefaultDisplayName, modelReference.Name);
        }

        [Test]
        public void Remove_ReturnsTrueWhenCollectableInCategory()
        {
            Assert.True(modelReference.Remove(StartCollectable));
        }

        [Test]
        public void Remove_ReturnsFalseWhenCollectableNotInCategory()
        {
            Assert.False(modelReference.Remove(NewCollectable));
        }

        [Test]
        public void Search_ReturnsArrayOfCollectablesWhichFulfillPredicate()
        {
            modelReference.Add(NewCollectable);
            EngineScrob.Instance(out CollectableWallet wallet);

            wallet.Add(StartCollectable);
            var result = modelReference.Search((item) => {
                return wallet.Contains(item);
            });

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(StartCollectable, result[0]);

            ScriptableObject.DestroyImmediate(wallet);
        }

        [Test]
        public void Search_ReturnsEmptyArrayWhenPredicateFails()
        {
            var result = modelReference.Search((item) => {
                return false;
            });

            Assert.Zero(result.Length);
        }

        [Test]
        public void Search_ReturnsAllCollectablesWhenNullPredicate()
        {
            modelReference.Add(NewCollectable);
            var result = modelReference.Search(null);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(StartCollectable, result[0]);
            Assert.AreEqual(NewCollectable, result[1]);
        }

        [Test]
        public void Value_ReturnsWorthOfCategory()
        {
            Assert.AreEqual(1, modelReference.Value);
        }

        [Test]
        public void Value_IncreasesWhenAddingElements()
        {
            modelReference.Add(NewCollectable);

            Assert.AreEqual(2, modelReference.Value);
        }

        [Test]
        public void Value_ReducesWhenRemovingElements()
        {
            modelReference.Remove(StartCollectable);

            Assert.Zero(modelReference.Value);
        }
        #endregion
    }
}