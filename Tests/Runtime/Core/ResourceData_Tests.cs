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
        public static IEnumerable UseCases_Constructor()
        {
            const int Zero = 0;
            const int One = 1;
            var positiveValue = Random.Range(1, 1000);
            var negativeValue = Random.Range(-1000, -1);

            yield return new object[]{ true, positiveValue, positiveValue };
            yield return new object[]{ false, positiveValue, One};
            yield return new object[]{ true, Zero, Zero};
            yield return new object[]{ false, Zero, One};
            yield return new object[]{ true, negativeValue, Zero};
            yield return new object[]{ false, negativeValue, One};
        }
        [TestCaseSource(nameof(UseCases_Constructor))]
        public void Constructor_InitializesWithResourceAndAmount(bool quantifiable, int input, int output)
        {
            var resource = Substitute.For<IResource>();
            resource.Quantifiable.Returns(quantifiable);

            modelReference = new ResourceData(resource, input);

            Assert.AreEqual(resource, modelReference.Resource);
            Assert.AreEqual(output, modelReference.Amount);
        }

        [Test]
        public void Resource_ReturnsResourceReference()
        {
            modelReference.Resource = ValueSubstitute(out IResource resource);

            Assert.AreEqual(resource, modelReference.Resource);
        }

        public static IEnumerable UseCases_Amount()
        {
            const int Zero = 0;
            const int One = 1;
            var positiveValue = Random.Range(1, 1000);
            var negativeValue = Random.Range(-1000, -1);

            yield return new object[]{ true, positiveValue, positiveValue };
            yield return new object[]{ false, positiveValue, One};
            yield return new object[]{ true, Zero, Zero};
            yield return new object[]{ false, Zero, One};
            yield return new object[]{ true, negativeValue, Zero};
            yield return new object[]{ false, negativeValue, One};
        }
        [TestCaseSource(nameof(UseCases_Amount))]
        public void Amount_ReturnsQuantityOfItem(bool quantifiable, int input, int output)
        {
            ValueSubstitute(out IResource resource);
            resource.Quantifiable.Returns(quantifiable);

            modelReference.Resource = resource;
            modelReference.Amount = input;

            Assert.AreEqual(output, modelReference.Amount);
        }
        #endregion
    }
}