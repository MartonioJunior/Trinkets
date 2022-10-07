using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceData_Tests: TestModel<ResourceData>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = new ResourceData();
        }

        public override void DestroyTestContext()
        {
            modelReference = default;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Constructor_InitializesWithResourceAndAmount()
        {
            var resource = Substitute.For<IResource>();
            var amount = 5;

            modelReference = new ResourceData(resource, amount);

            Assert.AreEqual(resource, modelReference.Resource);
            Assert.AreEqual(amount, modelReference.Amount);
        }

        [Test]
        public void Resource_ReturnsResourceReference()
        {
            modelReference.Resource = ValueSubstitute(out IResource resource);

            Assert.AreEqual(resource, modelReference.Resource);
        }

        public static IEnumerable UseCases_Amount()
        {
            var positiveAmount = Random.Range(1,1000);
            var negativeAmount = Random.Range(-1000,0);

            yield return new object[]{ true, positiveAmount, positiveAmount};
            yield return new object[]{ false, positiveAmount, 1};
            yield return new object[]{ true, negativeAmount, 0};
            yield return new object[]{ false, negativeAmount, 1};
        }
        [TestCaseSource(nameof(UseCases_Amount))]
        public void Amount_ReturnsQuantityOfItem(bool isQuantifiable, int input, int output)
        {
            ValueSubstitute(out IResource resource);
            resource.Quantifiable.Returns(isQuantifiable);

            modelReference.Resource = resource;
            modelReference.Amount = input;

            Assert.AreEqual(output, modelReference.Amount);
        }
        #endregion
    }
}