using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Collectables;
using MartonioJunior.Trinkets;
using System;
using System.Collections.Generic;
using NSubstitute;
using Random = UnityEngine.Random;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class CollectableWallet_Tests: ScrobTestModel<CollectableWallet>
    {
        #region TestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Add()
        {
            yield return new object[]{ Substitute<ICollectable>(), true, false };
            yield return new object[]{ null, false, false };
        }
        [TestCaseSource(nameof(UseCases_Add))]
        public void Add_InsertsCollectablesIntoGroup(ICollectable collectable, bool firstOutput, bool secondOutput)
        {
            var resourceData = new ResourceData(collectable);

            Assert.AreEqual(firstOutput, modelReference.Add(resourceData));
            Assert.AreEqual(secondOutput, modelReference.Add(resourceData));
        }

        public static IEnumerable UseCases_AmountOf()
        {
            yield return new object[]{ Substitute<ICollectable>(), Random.Range(-10000,10000), 1 };
            yield return new object[]{ null, Random.Range(-10000,10000), 0 };
        }
        [TestCaseSource(nameof(UseCases_AmountOf))]
        public void AmountOf_ReturnsCollectablePresenceInGroup(ICollectable collectable, int amount, int output)
        {
            modelReference.Add(new ResourceData(collectable, amount));

            Assert.AreEqual(output, modelReference.AmountOf(collectable));
        }

        [Test]
        public void Clear_RemovesAllResourcesFromGroup()
        {
            modelReference.Add(new ResourceData(Substitute(out ICollectable collectable)));

            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(collectable));
        }

        [Test]
        public void Contents_ReturnsCollectableGroup()
        {
            Assert.IsInstanceOf(typeof(CollectableGroup), modelReference.Contents);
            Assert.IsNotNull(modelReference.Contents);
        }

        public static IEnumerable UseCases_Remove()
        {
            var collectableA = Substitute<ICollectable>();
            var collectableB = Substitute<ICollectable>();

            yield return new object[]{ collectableA, collectableA, true, false };
            yield return new object[]{ collectableA, collectableB, false, true };
            yield return new object[]{ collectableA, null, false, true };
            yield return new object[]{ null, collectableA, false, false };
            yield return new object[]{ null, collectableB, false, false };
            yield return new object[]{ null, null, false, false };
        }
        [TestCaseSource(nameof(UseCases_Remove))]
        public void Remove_TakesAwayResourcesFromGroup(ICollectable startingCollectable, ICollectable collectableToRemove, bool output, bool stillAvailable)
        {
            modelReference.Add(new ResourceData(startingCollectable));

            Assert.AreEqual(output, modelReference.Remove(new ResourceData(collectableToRemove)));

            Assert.AreEqual(stillAvailable, modelReference.Contains(startingCollectable));
        }

        public static IEnumerable UseCases_Search()
        {
            var emptySource = Array<ResourceData>(0, null);
            var filledSource = Array<ResourceData>(Random.Range(1,10), Mock.Collectables);

            Predicate<IResourceData> predicate = (item) => item.Amount == 1;
            List<ResourceData> filteredData = new List<ResourceData>();
            foreach (var item in filledSource)
                if (predicate(item)) filteredData.Add(item);

            yield return new object[]{ emptySource, predicate, emptySource };
            yield return new object[]{ emptySource, null, emptySource };
            yield return new object[]{ filledSource, predicate, filteredData };
            yield return new object[]{ filledSource, null, filledSource };
        }
        [TestCaseSource(nameof(UseCases_Search))]
        public void Search_ReturnsResourcesWhichFulfillThePredicate(ICollection<ResourceData> resources, Predicate<IResourceData> predicate, ICollection<ResourceData> output)
        {
            modelReference.AddRange(resources);

            var result = modelReference.Search(predicate);

            CollectionAssert.AreEquivalent(output, result);
        }

        public static IEnumerable UseCases_AddFrom()
        {
            bool fixValues = true;
            var anyValue = Range(-10000,10000, defaultValue: 983, fixValues);
            int arraySize = Range(1, 10, defaultValue: 3, fixValues);
            int negativeValue = Range(-10000, -1, defaultValue: -384, fixValues);
            int smallerValue = Range(0, arraySize-1, defaultValue: 2, fixValues);
            int greaterValue = Range(arraySize, 10, defaultValue: 6, fixValues);

            yield return new object[]{ Array<ResourceData>(0, null), anyValue, 0 };
            yield return new object[]{ Array<ResourceData>(arraySize, Mock.Collectables), negativeValue, 0};
            yield return new object[]{ Array<ResourceData>(arraySize, Mock.Collectables), smallerValue, smallerValue };
            yield return new object[]{ Array<ResourceData>(arraySize, Mock.Collectables), greaterValue, arraySize};
        }
        [TestCaseSource(nameof(UseCases_AddFrom))]
        public void AddFrom_InsertResourcesFromSourceIntoGroup(ICollection<ResourceData> sourceData, int amountToAdd, int amountAdded)
        {
            var group = new CollectableGroup();
            group.AddRange(sourceData);

            Assert.AreEqual(amountAdded, modelReference.AddFrom(group, amountToAdd));

            int count = 0;
            foreach (var item in sourceData) {
                count += modelReference.Contains(item.Resource) ? 1 : 0;
            }
            Assert.AreEqual(amountAdded, count);
        }

        public static IEnumerable UseCases_RemoveFrom()
        {
            bool fixValues = true;
            var sizeA = 10;
            var sizeB = 6;
            var listA = Array<ResourceData>(sizeA, Mock.Collectables);
            var listB = Array<ResourceData>(sizeB, Mock.Collectables);

            var overlapAmount = Range(0, Mathf.Min(sizeA,sizeB), defaultValue: 5, fixValues);
            var anyAmount = Range(-10000, 10000, defaultValue: -728, fixValues);
            var amountInOverlapRange = Range(0, overlapAmount, defaultValue: 3, fixValues);
            var amountInListRange = Range(0, sizeA, defaultValue: 8, fixValues);
            var higherAmount = sizeA+sizeB;

            for (int i = 0; i < overlapAmount; i++) listB[i] = listA[i];

            yield return new object[]{ listA, null, anyAmount, 0 };
            yield return new object[]{ null, null, anyAmount, 0 };
            yield return new object[]{ null, listA, anyAmount, 0 };
            yield return new object[]{ listA, listB, amountInOverlapRange, amountInOverlapRange };
            yield return new object[]{ listA, listB, higherAmount, overlapAmount };
            yield return new object[]{ listA, listA, amountInListRange, amountInListRange };
            yield return new object[]{ listA, listA, higherAmount, sizeA };
        }
        [TestCaseSource(nameof(UseCases_RemoveFrom))]
        public void RemoveFrom_RemoveResourcesFromGroupPresentInSource(ICollection<ResourceData> modelData, ICollection<ResourceData> sourceData, int amountToRemove, int amountRemoved)
        {
            modelReference.AddRange(modelData);
            var group = new CollectableGroup();
            group.AddRange(sourceData);

            Assert.AreEqual(amountRemoved, modelReference.RemoveFrom(group, amountToRemove));

            if (modelData == null) return;

            int count = 0;
            foreach (var item in modelData) {
                count += modelReference.Contains(item.Resource) ? 1 : 0;
            }
            Assert.AreEqual(modelData.Count, count+amountRemoved);
        }
        #endregion
    }
}