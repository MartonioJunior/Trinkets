using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using MartonioJunior.Trinkets.Collectables;
using System;
using System.Collections.Generic;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceInstancerComponent_Tests: ComponentTestModel<ResourceInstancerComponent>
    {
        #region TestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.Source = Mock.CurrencyWallet;
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
        public void Data_ReturnsRequirementsOfComponent([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            modelReference.Data.AddRange(data);

            var result = modelReference.Data;

            CollectionAssert.AreEquivalent(data, result);
        }

        [Test]
        public void Source_ReturnsWalletWhichReceivesTheResources()
        {
            modelReference.Source = ValueSubstitute(out CurrencyWallet wallet);

            Assert.AreEqual(wallet, modelReference.Source);
        }

        [Test]
        public void AddTo_InsertsResourcesIntoGroup([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ResourceData[] input)
        {
            var group = Substitute.For<IResourceGroup>();
            modelReference.enabled = enabled;
            modelReference.Data.AddRange(input);

            modelReference.AddTo(group);

            if (enabled) {
                group.ReceivedWithAnyArgs(input.Length).Add(default);
                foreach(var item in input) {
                    group.Received().Add(item);
                }
            } else {
                group.DidNotReceiveWithAnyArgs().Add(default);
            }
        }

        [Test]
        public void AddTo_InvokesOnCollectedEventWhenResourceIsAdded([Values] bool enabled)
        {
            var wasCalled = false;
            var group = Substitute.For<IResourceGroup>();

            modelReference.enabled = enabled;
            modelReference.OnCollected += () => wasCalled = true;

            modelReference.AddTo(group);

            Assert.AreEqual(enabled, wasCalled);
        }

        [Test]
        public void AddTo_TakesAwayResourcesFromSuppliedWallet([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out IResourceGroup group);

            modelReference.AddTo(group);

            Assert.Ignore(IncompleteImplementation);
        }

        [Test]
        public void AddToWallet_WorksTheSameAsAddTo([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out Wallet wallet);
            modelReference.Data.AddRange(data);

            modelReference.AddToWallet(wallet);

            wallet.Received(data.Count).Add(Arg.Any<IResourceData>());
        }
        #endregion
    }
}