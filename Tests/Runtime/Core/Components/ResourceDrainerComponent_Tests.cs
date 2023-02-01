using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using System.Collections.Generic;
using NSubstitute;
using MartonioJunior.Trinkets.Collectables;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceDrainerComponent_Tests: ComponentTestModel<ResourceDrainerComponent>
    {
        #region TestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Test Preparation
        public static IEnumerable ResourceDataCases()
        {
            return Mock.ResourceDataCases();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Data_ReturnsRequirementsForComponent([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            modelReference.Data.AddRange(data);

            var result = modelReference.Data;

            CollectionAssert.AreEquivalent(data, result);
        }

        [Test]
        public void Destination_ReturnsWalletWhichReceivesTaxedResources()
        {
            modelReference.Destination = Substitute(out Wallet wallet);

            Assert.AreEqual(wallet, modelReference.Destination);
        }

        [Test]
        public void Tax_AddsRemovedResourcesToSuppliedWallet([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            Substitute(out IResourceGroup group);
            Substitute(out Wallet wallet);
            group.Remove(Arg.Any<IResourceData>()).Returns(true);

            modelReference.Data.AddRange(data);
            modelReference.enabled = enabled;
            modelReference.Destination = wallet;

            modelReference.Tax(group);

            if (enabled) {
                wallet.ReceivedWithAnyArgs(data.Count).Add(default);
            } else {
                wallet.DidNotReceiveWithAnyArgs().Add(default);
            }
        }

        [Test]
        public void Tax_InvokesOnDrainEventWhenResourcesAreRemoved([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            var wasCalled = false;
            var group = Substitute<IResourceGroup>();

            modelReference.Data.AddRange(data);
            modelReference.enabled = enabled;
            modelReference.OnDrain += () => wasCalled = true;
            
            modelReference.Tax(group);

            Assert.AreEqual(enabled, wasCalled);
        }

        [Test]
        public void Tax_RemovesResourcesFromAGroup([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            Substitute(out IResourceGroup group);
            modelReference.Data.AddRange(data);
            modelReference.enabled = enabled;

            modelReference.Tax(group);

            if (enabled) {
                group.ReceivedWithAnyArgs(data.Count).Remove(default);
            } else {
                group.DidNotReceiveWithAnyArgs().Remove(default);
            }
        }

        [Test]
        public void Drain_WorksTheSameAsTheTaxMethod([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            Substitute(out Wallet wallet);
            modelReference.Data.AddRange(data);
            modelReference.enabled = enabled;

            modelReference.Drain(wallet);

            if (enabled) {
                wallet.ReceivedWithAnyArgs(data.Count).Remove(default);
            } else {
                wallet.DidNotReceiveWithAnyArgs().Remove(default);
            }
        }
        #endregion
    }
}