using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemProcessor_Tests: ComponentTestModel<ItemProcessor_Dummy>
    {
        #region Constants
        private ItemModel_Dummy Model;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Model);
            
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Model = Model;
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Model);
            Model = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Model_ReturnsFilterForWalletSearch()
        {
            Assert.AreEqual(Model, modelReference.Model);
        }

        [Test]
        public void Convert_IItemWallet_ReturnsResultForProcessing()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Convert_IItemArray_CapturesValueForItemAtListenerIndex()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Convert_IItemArray_ReturnsDefaultWhenArrayIsNull()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Convert_IItemArray_ReturnsDefaultWhenListenerIndexOutOfRange()
        {
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}