using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using MartonioJunior.Trinkets.Collectables;
using NSubstitute;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceGroup_Tests: TestModel<ResourceGroup>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = new ResourceGroup();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Add_InsertsResourcesIntoGroup()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void AmountOf_ReturnsQuantityOfResourceInGroup()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Remove_DeletesResourceFromGroup()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Search_LooksForResourcesWhichFulfillPredicate()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Clear_RemovesAllResourcesFromGroup()
        {
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}