using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using MartonioJunior.Trinkets.Collectables;
using NSubstitute;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
        #region Test Preparation
        private static IResource MockIResource()
        {
            IResource resource = Substitute.For<IResource>();
            resource.Quantifiable.Returns(true);
            return resource;
        }
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Add()
        {
            bool fixValues = true;
            IResource resource = MockIResource();
            int positiveValue = Parameter.Range(1,10000, defaultValue: 9, fixValues);
            int negativeValue = Parameter.Range(-10000,-1, defaultValue: -45, fixValues);

            yield return new object[]{ resource, positiveValue, true };
            yield return new object[]{ null, positiveValue, false };
            yield return new object[]{ resource, negativeValue, false };
            yield return new object[]{ resource, 0, false };
        }
        [TestCaseSource(nameof(UseCases_Add))]
        public void Add_InsertsResourcesIntoGroup(IResource resource, int amount, bool output)
        {
            var data = new ResourceData(resource, amount);

            Assert.AreEqual(output, modelReference.Add(data));
        }

        public static IEnumerable UseCases_AmountOf()
        {
            bool fixValues = true;
            IResource resource = MockIResource();
            var positiveValue = Parameter.Range(1, 10000, defaultValue: 6, fixValues);
            int negativeValue = Parameter.Range(-10000, -1, defaultValue: -5, fixValues);
            int anyValue = Parameter.Range(-10000, 10000, defaultValue: 784, fixValues);

            yield return new object[]{ resource, positiveValue, positiveValue };
            yield return new object[]{ resource, negativeValue, 0 };
            yield return new object[]{ null, anyValue, 0 };
        }
        [TestCaseSource(nameof(UseCases_AmountOf))]
        public void AmountOf_ReturnsQuantityOfResourceInGroup(IResource resource, int input, int output)
        {
            modelReference.Add(new ResourceData(resource, input));

            Assert.AreEqual(output, modelReference.AmountOf(resource));
        }

        [Test]
        public void Clear_RemovesAllResourcesFromGroup()
        {
            modelReference.Add(new ResourceData(ValueSubstitute(out IResource resource), 8));

            modelReference.Clear();

            Assert.Zero(modelReference.AmountOf(resource));
        }

        public static IEnumerable UseCases_Remove()
        {
            bool fixValues = true;
            const int Limit = 10000;
            var initialValue = Parameter.Range(1, Limit, defaultValue: 32, fixValues);
            var lowerAmount = Parameter.Range(1, initialValue, defaultValue: 21, fixValues);
            var higherAmount = Parameter.Range(initialValue, Limit, defaultValue: 65, fixValues);
            var anyValue = Parameter.Range(-Limit, Limit, defaultValue: -89, fixValues);

            yield return new object[]{ initialValue, lowerAmount, true, initialValue-lowerAmount};
            yield return new object[]{ initialValue, higherAmount, true, 0 };
            yield return new object[]{ initialValue, -lowerAmount, false, initialValue };
            yield return new object[]{ initialValue, 0, false, initialValue };
            yield return new object[]{ null, anyValue, false, 0 };
        }
        [TestCaseSource(nameof(UseCases_Remove))]
        public void Remove_DeletesResourceFromGroup(int? input, int removeAmount, bool operationResult, int finalValue)
        {
            IResource resource = MockIResource();
            if (input is int startAmount) modelReference.Add(new ResourceData(resource, startAmount));

            Assert.AreEqual(operationResult, modelReference.Remove(new ResourceData(resource, removeAmount)));
            Assert.AreEqual(finalValue, modelReference.AmountOf(resource));
        }

        public static IEnumerable UseCases_Search()
        {
            var emptySource = Parameter.Array<ResourceData>(0, null);
            var validSource = Parameter.Array<ResourceData>(10, Mock.Currencies);

            Predicate<IResourceData> predicate = (item) => item.Amount > 1000;
            List<ResourceData> filteredData = new List<ResourceData>();
            foreach(var item in validSource)
                if (predicate(item)) filteredData.Add(item);

            yield return new object[]{ emptySource, predicate, emptySource };
            yield return new object[]{ emptySource, null, emptySource };
            yield return new object[]{ validSource, predicate, filteredData };
            yield return new object[]{ validSource, null, validSource };
        }
        [TestCaseSource(nameof(UseCases_Search))]
        public void Search_LooksForResourcesWhichFulfillPredicate(ICollection<ResourceData> resources, Predicate<IResourceData> predicate, ICollection<ResourceData> output)
        {
            modelReference.AddRange(resources);

            var result = modelReference.Search(predicate);

            CollectionAssert.AreEquivalent(output, result);
        }
        #endregion
    }
}