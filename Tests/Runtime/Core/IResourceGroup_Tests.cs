using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;
using System.Collections.Generic;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets
{
    public class IResourceGroup_Tests: TestModel<IResourceGroup>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Substitute<IResourceGroup>();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Contains()
        {
            var array = Array<ResourceData>(10, Mock.MixCurrenciesAndCollectables);
            var validItem = array[0];
            var invalidItem = new ResourceData();

            yield return new object[]{array, validItem, true};
            yield return new object[]{array, invalidItem, false};
            yield return new object[]{array, null, false};
            yield return new object[]{null, validItem, false};
        }
        [TestCaseSource(nameof(UseCases_Contains))]
        public void Contains_DetectsPresenceOfResourceInGroup(ResourceData[] groupData, ResourceData input, bool output)
        {
            var expectedResult = (groupData as ICollection<ResourceData>)?.Contains(input) ?? false;
            modelReference.AmountOf(Arg.Any<IResource>()).Returns(expectedResult ? 1 : 0);
            modelReference.AddRange(groupData);

            Assert.AreEqual(output, modelReference.Contains(input.Resource));
        }

        public static IEnumerable UseCases_Join()
        {
            var array = Array<ResourceData>(10, Mock.MixCurrenciesAndCollectables);
            var overlapArray = array.Clone() as ResourceData[];
            for(int i = 1; i < overlapArray.Length; i+=2) {
                overlapArray[i].Amount *= 2;
            }
            var empty = new ResourceData[0];

            yield return new object[]{ array[0..4], array[4..6], array[0..6] };
            yield return new object[]{ array[2..6], array[3..7], overlapArray[2..7] };
            yield return new object[]{ array, array, overlapArray };
            yield return new object[]{ null, array, array };
            yield return new object[]{ array, null, array };
            yield return new object[]{ null, null, empty };
        }
        [TestCaseSource(nameof(UseCases_Join))]
        public void Join_CombinesResourceFromTwoGroupsIntoANewOne(ResourceData[] groupDataA, ResourceData[] groupDataB, ResourceData[] output)
        {
            var groupA = new ResourceGroup(groupDataA);
            var groupB = new ResourceGroup(groupDataB);

            var result = groupA.Join(groupB);

            CollectionAssert.AreEquivalent(output, result.All());
        }

        public static IEnumerable UseCases_Overlap()
        {
            var array = Array<ResourceData>(10, Mock.MixCurrenciesAndCollectables);
            var empty = new ResourceData[0];

            yield return new object[]{ array[0..4], array[4..6], empty };
            yield return new object[]{ array[3..6], array[4..7], array[4..6] };
            yield return new object[]{ array, array, array };
            yield return new object[]{ null, array, empty };
            yield return new object[]{ array, null, empty };
            yield return new object[]{ null, null, empty };
        }
        [TestCaseSource(nameof(UseCases_Overlap))]
        public void Overlap_ReturnsAmountsPresentInBothGroups(ResourceData[] groupDataA, ResourceData[] groupDataB, ResourceData[] output)
        {
            var groupA = new ResourceGroup(groupDataA);
            var groupB = new ResourceGroup(groupDataB);

            var result = groupA.Overlap(groupB);

            CollectionAssert.AreEquivalent(output, result.All());
        }

        public static IEnumerable UseCases_Transfer()
        {
            var array = Array<ResourceData>(10, Mock.MixCurrenciesAndCollectables);
            var overlapArray = array.Clone() as ResourceData[];
            for(int i = 1; i < overlapArray.Length; i+=2) {
                overlapArray[i].Amount *= 2;
            }
            var empty = new ResourceData[0];

            yield return new object[]{ array[0..4], array[4..6], array[0..6], true };
            yield return new object[]{ array[2..6], array[3..7], overlapArray[2..7], true };
            yield return new object[]{ array, array, overlapArray, true };
            yield return new object[]{ null, array, array, false };
            yield return new object[]{ array, null, array, true };
            yield return new object[]{ null, null, empty, false };
        }
        [TestCaseSource(nameof(UseCases_Transfer))]
        public void Transfer_MovesResourcesFromOneGroupToDestination(ResourceData[] groupDataA, ResourceData[] groupDataB, ResourceData[] output, bool result)
        {
            var groupA = new ResourceGroup(groupDataA);
            var groupB = new ResourceGroup(groupDataB);

            Assert.AreEqual(result, groupA.Transfer(groupB));

            CollectionAssert.IsEmpty(groupA.All());
            CollectionAssert.AreEquivalent(output, groupB.All());
        }

        public static IEnumerable UseCases_Unique()
        {
            var array = Array<ResourceData>(10, Mock.MixCurrenciesAndCollectables);
            var empty = new ResourceData[0];

            yield return new object[]{ array[0..4], array[4..6], array[0..4] };
            yield return new object[]{ array[2..6], array[4..7], array[2..4] };
            yield return new object[]{ array, array, empty };
            yield return new object[]{ null, array, empty };
            yield return new object[]{ array, null, array };
            yield return new object[]{ null, null, empty };
        }
        [TestCaseSource(nameof(UseCases_Unique))]
        public void Unique_ReturnsGroupWithUniqueResourcesWhenComparedToAnother(ResourceData[] groupDataA, ResourceData[] groupDataB, ResourceData[] output)
        {
            var groupA = new ResourceGroup(groupDataA);
            var groupB = new ResourceGroup(groupDataB);

            var result = groupA.Unique(groupB);

            CollectionAssert.AreEquivalent(output, result.All());
        }
        #endregion
    }
}