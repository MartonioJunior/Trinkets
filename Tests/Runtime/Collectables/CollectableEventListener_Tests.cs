using System.Collections;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableEventListener_Tests: ComponentTestModel<CollectableEventListener>
    {
        #region Constants
        private CollectableWallet Wallet;
        private CollectableData Collectable;
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
            modelReference.Wallet = Wallet;
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
        public void Convert_ReturnsTrueWhenWalletHasSelectedCollectable()
        {
            Wallet.Add(Collectable);

            Assert.True(modelReference.Convert(Wallet));
        }

        [Test]
        public void Convert_ReturnsFalseWhenWalletIsEmpty()
        {
            Assert.False(modelReference.Convert(null));
        }

        [Test]
        public void Convert_ReturnsFalseWhenCollectableFieldIsEmpty()
        {
            Assert.False(modelReference.Convert(Wallet));
        }

        [Test]
        public void Convert_ReturnsFalseWhenCollectableNotInWallet()
        {
            Assert.False(modelReference.Convert(Wallet));
        }
        #endregion
        #region Coroutine Methods
        [UnityTest]
        public IEnumerator Start_InvokesOnAmountChangeEvent()
        {
            bool isActive = false;
            modelReference.onCollectableChange += (item) => isActive = item;
            Wallet.Add(Collectable);
            yield return new WaitForSeconds(CollectableEventListener.UpdateTime);

            Assert.True(isActive);
        }
        [UnityTest]
        public IEnumerator Start_UpdatesOnALoopBasedOnUpdateTime()
        {
            int triggerCount = 0;
            modelReference.onCollectableChange += (item) => triggerCount++;
            Wallet.Add(Collectable);

            yield return new WaitForSeconds(CollectableEventListener.UpdateTime/2);

            Assert.Zero(triggerCount);
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(1, triggerCount);
        }
        #endregion
    }
}