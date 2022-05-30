using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemDropComponent_Tests: ComponentTestModel<ItemDropComponent>
    {
        #region Constants
        private ItemData_Dummy Gem;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Gem);
            EngineScrob.Instance(out Wallet);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Item = Gem;
        }
        
        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Gem);
            ScriptableObject.DestroyImmediate(Wallet);
            
            Gem = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void AddTo_InstancesItemOnWallet()
        {
            modelReference.AddTo(Wallet);

            Assert.AreEqual(1, Wallet.AmountOf(Gem));
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
        #endregion
    }
}