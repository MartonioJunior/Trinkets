using System.Collections;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableComponent_Tests: ComponentTestModel<CollectableComponent>
    {
        #region Constants
        private CollectableData Collectable;
        private CollectableWallet Wallet;
        #endregion
        #region ComponentTestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Wallet);
            EngineScrob.Instance(out Collectable);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Collectable = Collectable;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();

            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(Collectable);

            Wallet = null;
            Collectable = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void AddTo_InsertsCollectableIntoWallet()
        {
            modelReference.AddTo(Wallet);

            Assert.True(Wallet.Contains(Collectable));
        }

        [Test]
        public void AddTo_InvokesEventWhenAdditionSuccessful()
        {
            bool addedSuccessfully = false;
            modelReference.onCollected += (item) => addedSuccessfully = item;
            modelReference.AddTo(Wallet);

            Assert.True(addedSuccessfully);
            modelReference.AddTo(Wallet);

            Assert.False(addedSuccessfully);
        }

        [Test]
        public void AddTo_DoesNothingWhenComponentIsDisabled()
        {
            modelReference.enabled = false;
            modelReference.AddTo(Wallet);

            Assert.False(Wallet.Contains(Collectable));
        }

        [Test]
        public void AddTo_DoesNothingWhenCollectableIsNotSet()
        {
            modelReference.Collectable = null;
            modelReference.AddTo(Wallet);

            Assert.False(Wallet.Contains(Collectable));
        }

        [Test]
        public void AddToWallet_WorksTheSameAsAddTo()
        {
            modelReference.AddToWallet(Wallet);

            Assert.True(Wallet.Contains(Collectable));
        }
        #endregion
    }
}