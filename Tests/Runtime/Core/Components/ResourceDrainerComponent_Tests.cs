using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using System.Collections.Generic;
using NSubstitute;
using MartonioJunior.Trinkets.Collectables;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceDrainerComponent_Tests: ComponentTestModel<ResourceDrainerComponent>
    {
        #region TestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.Destination = Mock.CollectableWallet;
        }
        #endregion
        #region Test Preparation
        public static IEnumerable ResourceDataCases()
        {
            return Mock.ResourceDataCases();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Data_ReturnsRequirementsForComponent([ValueSource(nameof(ResourceDataCases))]ICollection<ResourceData> data)
        {
            modelReference.Data.AddRange(data);

            var result = modelReference.Data;

            CollectionAssert.AreEquivalent(data, result);
        }

        [Test]
        public void Destination_ReturnsWalletWhichReceivesTaxedResources()
        {
            modelReference.Destination = ValueSubstitute(out CollectableWallet wallet);

            Assert.AreEqual(wallet, modelReference.Destination);
        }

        [Test]
        public void Tax_AddsRemovedResourcesToSuppliedWallet()
        {
            ValueSubstitute(out IResourceGroup group);

            modelReference.Tax(group);

            Assert.Ignore(IncompleteImplementation);
        }

        [Test]
        public void Tax_InvokesOnDrainEventWhenResourcesAreRemoved([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            var wasCalled = false;
            var group = Substitute.For<IResourceGroup>();

            modelReference.enabled = enabled;
            modelReference.OnDrain += () => wasCalled = true;
            
            modelReference.Tax(group);

            Assert.AreEqual(enabled, wasCalled);
        }

        [Test]
        public void Tax_RemovesResourcesFromAGroup([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out IResourceGroup group);

            modelReference.Tax(group);

            group.Received(data.Count).Remove(Arg.Any<IResourceData>());
        }

        [Test]
        public void Drain_WorksTheSameAsTheTaxMethod([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out IResourceGroup group);

            modelReference.Tax(group);

            group.Received(data.Count).Remove(Arg.Any<IResourceData>());
        }
        #endregion
    }
}