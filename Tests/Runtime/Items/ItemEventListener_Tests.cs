using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;
using MartonioJunior.Collectables;
using Dummy = Tests.MartonioJunior.Collectables.Items.ItemEventListener_Dummy;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemEventListener_Tests: ComponentTestModel<ItemEventListener_Dummy>
    {
        #region Constants
        private ItemData_Dummy Shovel;
        private ItemWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Wallet);
            EngineScrob.Instance(out Shovel);

            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Item = Shovel;
            modelReference.Wallet = Wallet;

            Wallet.Add(Shovel);
        }

        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(Shovel);

            Shovel = null;
            Wallet = null;

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Convert_ReturnsValueFromResultOfEvents()
        {
            Assert.AreEqual(1, modelReference.Convert(Wallet));
            Assert.Zero(modelReference.Convert(null));
        }
        #endregion
        #region Coroutines
        [UnityTest]
        public IEnumerator Start_UpdatesResultEveryUpdateCycle()
        {
            int amount = -1;
            modelReference.onItemChange += (number) => amount = number;

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.AreEqual(1, amount);
            Wallet.Add(Shovel);

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.AreEqual(2, amount);
        }

        [UnityTest]
        public IEnumerator Start_InvokesOnItemChangeEvent()
        {
            bool triggeredEvent = false;
            modelReference.onItemChange += (number) => triggeredEvent = true;
            Assert.False(triggeredEvent);

            yield return new WaitForSeconds(Dummy.UpdateTime);
            Assert.True(triggeredEvent);
        }
        #endregion
    }
}