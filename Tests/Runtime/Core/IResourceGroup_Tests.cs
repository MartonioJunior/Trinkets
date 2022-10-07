using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;

namespace Tests.MartonioJunior.Trinkets
{
    public class IResourceGroup_Tests: TestModel<IResourceGroup>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Substitute.For<IResourceGroup>();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Contains_DetectsPresenceOfResourceInGroup()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Join_CombinesResourceFromTwoGroupsIntoANewOne()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Overlap_ReturnsAmountsPresentInBothGroups()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Transfer_MovesResourcesFromOneGroupToDestination()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Unique_ReturnsGroupWithUniqueResourcesWhenComparedToAnother()
        {
            Assert.Ignore(NotImplemented);
        }
        #endregion
    }
}