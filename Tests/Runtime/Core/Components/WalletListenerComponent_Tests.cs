using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;

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
            modelReference.Wallet = ValueSubstitute(out Wallet wallet);

            Assert.AreEqual(wallet, modelReference.Wallet);
        }

        [Test]
        public void Resource_ReturnsElementToLookFor()
        {
            modelReference.Resource = ValueSubstitute(out Resource resource);

            Assert.AreEqual(resource, modelReference.Resource);
        }

        [UnityTest]
        public IEnumerator Start_AlwaysInvokesOnAmountChangeEvent()
        {
            const int CheckValue = -1;
            var wait = new WaitForSeconds(WalletListenerComponent.UpdateTime);
            var expectedValue = Parameter.Range(1,10000, defaultValue: 5);
            int eventValue = CheckValue;
            modelReference.OnAmountChange += (amount) => eventValue = amount;
            yield return null;

            Assert.AreEqual(CheckValue, eventValue);
            modelReference.Wallet = ValueSubstitute(out Wallet wallet);
            yield return wait;

            Assert.Zero(eventValue);
            modelReference.Resource = ValueSubstitute(out Resource resource);
            wallet.AmountOf(resource).Returns(expectedValue);
            yield return wait;

            Assert.AreEqual(expectedValue, eventValue);
        }

        [UnityTest]
        public IEnumerator Start_AlwaysInvokesOnPresenceUpdateEvent()
        {
            var wait = new WaitForSeconds(WalletListenerComponent.UpdateTime);
            var expectedValue = Parameter.Range(1,10000, defaultValue: 5);
            bool? eventValue = null;
            modelReference.OnPresenceUpdate += (isPresent) => eventValue = isPresent;
            yield return null;

            Assert.Null(eventValue);
            modelReference.Wallet = ValueSubstitute(out Wallet wallet);
            yield return wait;

            Assert.False(eventValue);
            modelReference.Resource = ValueSubstitute(out Resource resource);
            wallet.AmountOf(resource).Returns(expectedValue);
            yield return wait;

            Assert.True(eventValue);
        }
        #endregion
    }
}