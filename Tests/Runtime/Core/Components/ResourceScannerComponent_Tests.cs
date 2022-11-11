using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using NSubstitute;
using MartonioJunior.Trinkets.Collectables;
using System.Collections.Generic;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceScannerComponent_Tests: ComponentTestModel<ResourceScannerComponent>
    {
        #region ComponentTestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Test Preparation
        public static IEnumerable ResourceDataCases()
        {
            return Mock.ResourceDataCases();
        }

        public IResourceGroup CreateGroup(ResourceData[] data)
        {
            var group = new ResourceGroup();
            group.AddRange(data);
            return group;
        }

        public void SetModelParameters(bool enabled, ResourceData[] requirements)
        {
            modelReference.enabled = enabled;
            modelReference.Data.AddRange(requirements);
        }
        #endregion
        #region Method Tests
        [Test]
        public void Data_ReturnsListOfRequiredResources([ValueSource(nameof(ResourceDataCases))] ResourceData[] data)
        {
            modelReference.Data.AddRange(data);

            CollectionAssert.AreEquivalent(data, modelReference.Data);
        }

        [Test]
        public void Destination_DefinesWalletToReceiveTaxedResources()
        {
            modelReference.Destination = ValueSubstitute(out Wallet wallet);
            
            Assert.AreEqual(wallet, modelReference.Destination);
        }

        public static IEnumerable UseCases_Check()
        {
            var empty = new ResourceData[0];
            var list = Parameter.Array<ResourceData>(Random.Range(4,10), Mock.MixCurrenciesAndCollectables);
            var insufficient = new ResourceData[3]{ list[0], list[2], list[list.Length-1] };
            var sufficient = list;
            var anything = Parameter.Array<ResourceData>(Random.Range(1,20), Mock.MixCurrenciesAndCollectables);

            yield return new object[]{ true, empty, anything, true };
            yield return new object[]{ true, list, insufficient, false };
            yield return new object[]{ true, list, sufficient, true };
            yield return new object[]{ false, anything, anything, false };
        }
        [TestCaseSource(nameof(UseCases_Check))]
        public void Check_VerifiesTheContentsInsideOfAGroup(bool enabled, ResourceData[] requirements, ResourceData[] input, bool output)
        {
            var group = CreateGroup(input);

            SetModelParameters(enabled, requirements);

            Assert.AreEqual(output, modelReference.Check(group));
        }

        [TestCaseSource(nameof(UseCases_Check))]
        public void Scan_VerifiesTheContentsInsideAGroup(bool enabled, ResourceData[] requirements, ResourceData[] input, bool output)
        {
            var group = CreateGroup(input);

            SetModelParameters(enabled, requirements);

            Assert.AreEqual(output, modelReference.Scan(group));
        }

        [TestCaseSource(nameof(UseCases_Check))]
        public void Scan_AlwaysInvokesOnScanEvent(bool enabled, ResourceData[] requirements, ResourceData[] input, bool output)
        {
            bool scanWasSuccessful = false;
            var group = CreateGroup(input);

            SetModelParameters(enabled, requirements);

            modelReference.OnScan += (result) => scanWasSuccessful = result;
            modelReference.Scan(group);

            Assert.AreEqual(output, scanWasSuccessful);
        }

        [TestCaseSource(nameof(UseCases_Check))]
        public void Tax_AlwaysInvokesOnTaxEvent(bool enabled, ResourceData[] requirements, ResourceData[] input, bool output)
        {
            bool wasTriggered = false;
            var group = CreateGroup(input);
    
            SetModelParameters(enabled, requirements);

            modelReference.OnTax += () => wasTriggered = true;
            modelReference.Tax(group);

            Assert.AreEqual(enabled, wasTriggered);
        }

        [Test]
        public void Tax_RemovesResourceFromTheWallet([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out IResourceGroup group);
            modelReference.Data.AddRange(data);

            modelReference.Tax(group);

            group.ReceivedWithAnyArgs(data.Count).Remove(default);
        }

        public static IEnumerable UseCases_ScanWallet()
        {
            var empty = new ResourceData[0];
            var list = Parameter.Array<ResourceData>(Random.Range(4,10), Mock.Collectables);
            var insufficient = new ResourceData[3]{ list[0], list[2], list[list.Length-1] };
            var sufficient = list;
            var anything = Parameter.Array<ResourceData>(Random.Range(1,20), Mock.Collectables);

            yield return new object[]{ true, empty, anything, true };
            yield return new object[]{ true, list, insufficient, false };
            yield return new object[]{ true, list, sufficient, true };
            yield return new object[]{ false, anything, anything, false };
        }
        [TestCaseSource(nameof(UseCases_ScanWallet))]
        public void ScanWallet_IsAVoidVersionOfScan(bool enabled, ResourceData[] requirements, ResourceData[] input, bool output)
        {
            var wallet = Mock.CollectableWallet;
            wallet.AddRange(input);
            bool result = false;

            SetModelParameters(enabled, requirements);

            modelReference.OnScan += (value) => result = value;
            modelReference.Scan(wallet);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void TaxWallet_WorksTheSameAsTax([ValueSource(nameof(ResourceDataCases))] ICollection<ResourceData> data)
        {
            ValueSubstitute(out IResourceGroup group);
            modelReference.Data.AddRange(data);

            modelReference.Tax(group);

            group.ReceivedWithAnyArgs(data.Count).Remove(default);
        }
        #endregion
    }
}