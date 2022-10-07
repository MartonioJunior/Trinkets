using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using NSubstitute;
using MartonioJunior.Trinkets.Collectables;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceScannerComponent_Tests: ComponentTestModel<ResourceScannerComponent>
    {
        #region ComponentTestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.Destination = Mock.CurrencyWallet;
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
        public void Data_ReturnsListOfRequiredResources([ValueSource(nameof(ResourceDataCases))] ResourceData[] data)
        {
            modelReference.Data.AddRange(data);

            foreach(var item in data) {
                Assert.True(modelReference.Data.Contains(item));
            }
        }

        [Test]
        public void Destination_DefinesWalletToReceiveTaxedResources()
        {
            modelReference.Destination = ValueSubstitute(out CollectableWallet wallet);
            
            Assert.AreEqual(wallet, modelReference.Destination);
        }

        [Test]
        public void Check_VerifiesTheContentsInsideOfAGroup([Values] bool enabled, [ValueSource(nameof(ResourceDataCases))] ResourceData[] input, bool output)
        {
            var group = Substitute.For<IResourceGroup>();

            modelReference.enabled = enabled;
            modelReference.Data.AddRange(input);

            Assert.AreEqual(output, modelReference.Check(group));
        }

        [Test]
        public void Scan_VerifiesTheContentsInsideAGroup()
        {
            var group = Substitute.For<IResourceGroup>();

            modelReference.Scan(group);

            Assert.Ignore(IncompleteImplementation);
        }

        [Test]
        public void Scan_AlwaysInvokesOnScanEvent()
        {
            bool scanWasSuccessful = false;
            var group = Substitute.For<IResourceGroup>();

            modelReference.OnScan += (result) => scanWasSuccessful = result;
            modelReference.Scan(group);

            Assert.True(scanWasSuccessful);
            Assert.Ignore(IncompleteImplementation);
        }

        [Test]
        public void Tax_AlwaysInvokesOnTaxEvent([Values] bool enabled)
        {
            bool wasTriggered = false;
            var group = Substitute.For<IResourceGroup>();
    
            modelReference.OnTax += () => wasTriggered = true;
            modelReference.enabled = enabled;
            modelReference.Tax(group);

            Assert.AreEqual(enabled, wasTriggered);
            Assert.Ignore(IncompleteImplementation);
        }

        [Test]
        public void Tax_RemovesResourceFromTheWallet()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void ScanWallet_WorksTheSameAsScan()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void TaxWallet_WorksTheSameAsTax()
        {
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}