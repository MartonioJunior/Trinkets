using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Collectables;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Collectables
{
    public class CollectableData_Tests: ScrobTestModel<CollectableData>
    {
        #region Constants
        private CollectableCategory Category;
        private CollectableWallet Wallet;
        #endregion
        #region ScrobTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Category);
            EngineScrob.Instance(out Wallet);
            
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
        public void Collect_InsertsCollectableIntoWallet()
        {
            modelReference.Collect(Wallet);

            Assert.True(Wallet.Contains(modelReference));
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