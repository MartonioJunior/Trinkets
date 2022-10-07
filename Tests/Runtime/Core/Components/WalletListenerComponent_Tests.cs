using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets
{
    public class WalletListenerComponent_Tests: ComponentTestModel<WalletListenerComponent>
    {
        #region TestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        [Test]
        public void Wallet_ReturnsDataSourceForComponent()
        {
            modelReference.Wallet = Value(Mock.CollectableWallet, out var wallet);

            Assert.AreEqual(wallet, modelReference.Wallet);
        }

        [Test]
        public void Resource_ReturnsElementToLookFor()
        {
            modelReference.Resource = Value(Mock.Currency("Test"), out var currency);

            Assert.AreEqual(currency, modelReference.Resource);
        }

        [UnityTest]
        public IEnumerator Start_UpdatesListenerInAConstantPeriod()
        {
            yield return null;
            Assert.Ignore(NotImplemented);
        }

        [UnityTest]
        public IEnumerator Start_AlwaysInvokesOnAmountChangeEvent()
        {
            yield return null;
            Assert.Ignore(NotImplemented);
        }

        [UnityTest]
        public IEnumerator Start_AlwaysInvokesOnPresenceUpdateEvent()
        {
            yield return null;
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}