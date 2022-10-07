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
            yield return new object[]{ Substitute.For<ICollectable>(), true, false };
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
            yield return new object[]{ Substitute.For<ICollectable>(), Random.Range(-10000,10000), 1 };
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
            modelReference.Add(new ResourceData(ValueSubstitute(out ICollectable collectable)));

            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(collectable));
        }

        public static IEnumerable UseCases_Remove()
        {
            var collectableA = Substitute.For<ICollectable>();
            var collectableB = Substitute.For<ICollectable>();

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
            var emptySource = Parameter.Array<ResourceData>(0, null);
            var filledSource = Parameter.Array<ResourceData>(Random.Range(1,10), Mock.Collectables);

            Predicate<IResourceData> predicate = (item) => item.Amount == 1;
            List<ResourceData> filteredData = new List<ResourceData>();
            foreach(var item in filledSource)
                if (predicate(item)) filteredData.Add(item);

            yield return new object[]{ emptySource, predicate, emptySource };
            yield return new object[]{ emptySource, null, emptySource };
            yield return new object[]{ filledSource, predicate, filteredData };
            yield return new object[]{ filledSource, null, filledSource };
        }
        [TestCaseSource(nameof(UseCases_Search))]
        public void Search_ReturnsResourcesWhichFulfillThePredicate(ICollection<ResourceData> resources, Predicate<IResourceData> predicate, ICollection<ResourceData> output)
        {
            foreach (var resource in resources) {
                modelReference.Add(resource);
            }

            var result = modelReference.Search(predicate);

            CollectionAssert.AreEquivalent(output, result);
        }

        public static IEnumerable UseCases_AddFrom()
        {
            yield return new object[]{ Parameter.Array<ResourceData>(0, null), UnityEngine.Random.Range(-10000,10000), 0 };
            yield return new object[]{ Parameter.Array<ResourceData>(Random.Range(1,10), Mock.Collectables), Random.Range(-10000,-1), 0};
            yield return new object[]{ Parameter.Array<ResourceData>(3, Mock.Collectables), 2, 2 };
            yield return new object[]{ Parameter.Array<ResourceData>(10, Mock.Currencies), Random.Range(4,10), 4};
        }
        [TestCaseSource(nameof(UseCases_AddFrom))]
        public void AddFrom_InsertResourcesFromSourceIntoGroup(ICollection<ResourceData> sourceData, int amountToAdd, int amountAdded)
        {
            var group = new CollectableGroup();
            foreach(var item in sourceData) group.Add(item);

            Assert.AreEqual(amountAdded, modelReference.AddFrom(group, amountToAdd));

            int count = 0;
            foreach(var item in sourceData) {
                count += modelReference.Contains(item.Resource) ? 1 : 0;
            }
            Assert.AreEqual(amountAdded, count);
        }

        public static IEnumerable UseCases_RemoveFrom()
        {
            var sizeA = 10;
            var sizeB = 6;
            var listA = Parameter.Array<ResourceData>(sizeA, Mock.Collectables);
            var listB = Parameter.Array<ResourceData>(sizeB, Mock.Collectables);

            var overlapAmount = Random.Range(0, Mathf.Min(sizeA,sizeB));
            var anyAmount = Random.Range(-10000,10000);
            var amountInOverlapRange = Random.Range(0,overlapAmount);
            var amountInListRange = Random.Range(0,sizeA);
            var higherAmount = sizeA+sizeB;

            for(int i = 0; i < overlapAmount; i++) listB[i] = listA[i];

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
            foreach(var item in modelData) modelReference.Add(item);
            var group = new CollectableGroup();
            foreach(var element in sourceData) group.Add(element);

            Assert.AreEqual(amountRemoved, modelReference.RemoveFrom(group, amountToRemove));

            int count = 0;
            foreach(var item in modelData) {
                count += modelReference.Contains(item.Resource) ? 1 : 0;
            }
            Assert.AreEqual(modelData.Count, count+amountRemoved);
        }
        #endregion
    }
}