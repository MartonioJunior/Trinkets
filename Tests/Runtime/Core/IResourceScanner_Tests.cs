using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Core
{
    public class IResourceScanner_Tests: TestModel<IResourceScanner>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Substitute.For<IResourceScanner>();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        [TestCase(true,true,1)]
        [TestCase(false,true,0)]
        [TestCase(true,false,0)]
        public void Scan_ChecksForResourcesInsideGroup(bool scanResult, bool taxesOnScan, int taxCalls)
        {
            int counter = 0;
            var group = Substitute.For<IResourceGroup>().With(Substitute.For<IResourceData>());

            modelReference.Check(group).Returns(scanResult);
            modelReference.When(x => x.Tax(Arg.Any<IResourceGroup>())).Do(x => counter++);
            modelReference.TaxGroupOnScan = taxesOnScan;

            Assert.AreEqual(scanResult, modelReference.Scan(group));
            Assert.AreEqual(taxCalls, counter);
        }
        #endregion
    }
}