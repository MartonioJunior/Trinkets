using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;
using MartonioJunior.Trinkets;
using Dummy = Tests.MartonioJunior.Trinkets.Items.ItemEventListener_Dummy;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemEventListener_Tests: ComponentTestModel<ItemEventListener_Dummy>
    {
        #region Constants
        private ItemModel_Dummy ShovelModel;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Wallet);
            EngineScrob.Instance(out ShovelModel);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Model = ShovelModel;
            modelReference.Wallet = Wallet;

            ShovelModel.AddTo(Wallet);
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(ShovelModel);

            ShovelModel = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Convert_ReturnsValueFromResultOfEvents()
        {
            Assert.AreEqual(1, modelReference.Convert(Wallet.SearchOn(ShovelModel, null))[0]);
            Assert.Zero(modelReference.Convert(null)[0]);
        }
        #endregion
        #region Coroutines
        [UnityTest]
        public IEnumerator Start_UpdatesResultEveryUpdateCycle()
        {
            int amount = -1;
            modelReference.onCollectionChange += (number) => amount = number[0];

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.AreEqual(1, amount);
            ShovelModel.AddTo(Wallet);

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.AreEqual(2, amount);
        }

        [UnityTest]
        public IEnumerator Start_InvokesOnItemChangeEvent()
        {
            bool triggeredEvent = false;
            modelReference.onCollectionChange += (number) => triggeredEvent = true;
            Assert.False(triggeredEvent);

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.True(triggeredEvent);
        }
        #endregion
    }
}