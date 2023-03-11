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
using Random = UnityEngine.Random;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Components
{
    public class ResourceInstancerComponent_Tests: ComponentTestModel<ResourceInstancerComponent>
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
        public void Data_ReturnsRequirementsOfComponent([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            modelReference.Data.AddRange(data);

            var result = modelReference.Data;

            CollectionAssert.AreEquivalent(data, result);
        }

        [Test]
        public void Source_ReturnsWalletWhichReceivesTheResources()
        {
            modelReference.Source = Substitute(out CurrencyWallet wallet);

            Assert.AreEqual(wallet, modelReference.Source);
        }

        [Test]
        public void AddTo_InsertsResourcesIntoGroup([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ResourceData[] input)
        {
            Substitute(out IResourceGroup group);
            modelReference.enabled = enabled;
            modelReference.Data.AddRange(input);

            modelReference.AddTo(group);

            if (enabled) {
                group.ReceivedWithAnyArgs(input.Length).Add(default);
                foreach (var item in input) {
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
            Substitute(out IResourceGroup group);

            modelReference.enabled = enabled;
            modelReference.OnCollected += () => wasCalled = true;

            modelReference.AddTo(group);

            Assert.AreEqual(enabled, wasCalled);
        }

        [Test]
        public void AddTo_TakesAwayResourcesFromSuppliedWallet([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            Substitute(out IResourceGroup group);
            Substitute(out Wallet wallet);

            modelReference.enabled = enabled;
            modelReference.Data.AddRange(data);
            modelReference.Source = wallet;

            modelReference.AddTo(group);

            if (enabled) {
                wallet.ReceivedWithAnyArgs(data.Count).Remove(default);
            } else {
                wallet.DidNotReceiveWithAnyArgs().Remove(default);
            }
        }

        [Test]
        public void AddToWallet_WorksTheSameAsAddTo([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            Substitute(out Wallet wallet);
            modelReference.enabled = enabled;
            modelReference.Data.AddRange(data);

            modelReference.AddToWallet(wallet);

            if (enabled) {
                wallet.ReceivedWithAnyArgs(data.Count).Add(default);
            } else {
                wallet.DidNotReceiveWithAnyArgs().Add(default);
            }
        }
        #endregion
    }
}