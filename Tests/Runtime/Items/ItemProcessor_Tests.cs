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
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Model);
            EngineScrob.Instance(out Wallet);
            
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            Wallet.InstanceMultiple(Model, 3);
            modelReference.Model = Model;
            modelReference.ListenerIndex = 1;
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Model);
            ScriptableObject.DestroyImmediate(Wallet);

            Model = null;
            Wallet = null;

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
        public void ListenerIndex_ReturnsIndexOfComponent()
        {
            Assert.AreEqual(1, modelReference.ListenerIndex);
        }

        [Test]
        public void ProcessArray_CapturesValueForItemAtListenerIndex()
        {
            var results = Wallet.SearchOn(Model, null);

            Assert.AreEqual(results[modelReference.ListenerIndex].Value, modelReference.ProcessArray(results));
        }

        [Test]
        public void ProcessArray_ReturnsDefaultWhenArrayIsNull()
        {
            Assert.AreEqual(default(int), modelReference.ProcessArray(null));
        }

        [Test]
        public void ProcessArray_ReturnsDefaultWhenListenerIndexOutOfRange()
        {
            var results = Wallet.SearchOn(Model, null);

            modelReference.ListenerIndex = 6;
            Assert.AreEqual(default(int), modelReference.ProcessArray(results));
        }

        [Test]
        public void ConvertWallet_ReturnsResultForProcessing()
        {
            var results = Wallet.SearchOn(Model, null);

            Assert.AreEqual(results[modelReference.ListenerIndex].Value, modelReference.Convert(Wallet));
        }
        #endregion
    }
}