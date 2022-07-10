using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemBuilderComponent_Tests: ComponentTestModel<ItemBuilderComponent>
    {
        #region Constants
        private ItemModel_Dummy GemModel;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out GemModel);
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Builder = GemModel;
        }
        
        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(GemModel);
            ScriptableObject.DestroyImmediate(Wallet);
            
            GemModel = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void AddTo_InstancesItemOnWallet()
        {
            modelReference.AddTo(Wallet);

            Assert.AreEqual(1, Wallet.AmountOf(GemModel));
        }

        [Test]
        public void AddTo_DoesNothingWhenWalletIsNull()
        {
            bool triggeredEvent = false;
            modelReference.onCollectedItem += () => triggeredEvent = true;
            modelReference.AddTo(null);

            Assert.False(triggeredEvent);
        }

        [Test]
        public void AddTo_InvokesOnCollectedItemEvent()
        {
            bool triggeredEvent = false;
            modelReference.onCollectedItem += () => triggeredEvent = true;
            modelReference.AddTo(Wallet);

            Assert.True(triggeredEvent);
        }

        [Test]
        public void Model_ReturnsReferenceForBuildingItems()
        {
            Assert.AreEqual(GemModel, modelReference.Model);
            Assert.AreEqual(GemModel, modelReference.Builder);
        }

        [Test]
        public void Model_ReturnsNullWhenNoBuilderIsSet()
        {
            modelReference.Builder = null;
            Assert.Null(modelReference.Builder);
        }
        #endregion
    }
}