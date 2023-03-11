using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Core
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
            var quantifiableResource = Mock.IResource(true);
            var uniqueResource = Mock.IResource(false);

            yield return new object[]{ quantifiableResource, positiveValue, positiveValue };
            yield return new object[]{ quantifiableResource, Zero, Zero };
            yield return new object[]{ quantifiableResource, negativeValue, Zero };
            yield return new object[]{ uniqueResource, positiveValue, One };
            yield return new object[]{ uniqueResource, Zero, One };
            yield return new object[]{ uniqueResource, negativeValue, One };
        }
        [TestCaseSource(nameof(UseCases_Constructor))]
        public void Constructor_InitializesWithResourceAndAmount(IResource resource, int input, int output)
        {
            modelReference = new ResourceData(resource, input);

            Assert.AreEqual(resource, modelReference.Resource);
            Assert.AreEqual(output, modelReference.Amount);
        }

        [Test]
        public void Resource_ReturnsResourceReference()
        {
            modelReference.Resource = Substitute(out IResource resource);

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
            Substitute(out IResource resource);
            resource.Quantifiable.Returns(quantifiable);

            modelReference.Resource = resource;
            modelReference.Amount = input;

            Assert.AreEqual(output, modelReference.Amount);
        }

        public static IEnumerable Operator_Add_UseCases()
        {
            var positiveValue = Random.Range(0,10000);
            var negativeValue = -positiveValue-1;
            var quantifiableResource = Mock.IResource(true);
            var uniqueResource = Mock.IResource(false);

            yield return new object[4]{ quantifiableResource, positiveValue, positiveValue, 2*positiveValue };
            yield return new object[4]{ quantifiableResource, positiveValue, negativeValue, 0 };
            yield return new object[4]{ quantifiableResource, negativeValue, positiveValue, positiveValue };
            yield return new object[4]{ uniqueResource, positiveValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, positiveValue, negativeValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, negativeValue, 1 };
        }
        [TestCaseSource(nameof(Operator_Add_UseCases))]
        public void Operator_Add_IncreasesAmountOnData(IResource resource, int inputAmount, int operatorAmount, int outputAmount)
        {
            modelReference = new ResourceData(resource, inputAmount);

            var result = modelReference + operatorAmount;

            Assert.AreEqual(outputAmount, result.Amount);
        }

        public static IEnumerable Operator_Subtract_UseCases()
        {
            var positiveValue = Random.Range(0,10000);
            var negativeValue = -positiveValue-1;
            var quantifiableResource = Mock.IResource(true);
            var uniqueResource = Mock.IResource(false);

            yield return new object[4]{ quantifiableResource, positiveValue, positiveValue, 0 };
            yield return new object[4]{ quantifiableResource, positiveValue, negativeValue, Mathf.Max(0, positiveValue-negativeValue) };
            yield return new object[4]{ quantifiableResource, negativeValue, positiveValue, 0 };
            yield return new object[4]{ uniqueResource, positiveValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, positiveValue, negativeValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, negativeValue, 1 };
        }
        [TestCaseSource(nameof(Operator_Subtract_UseCases))]
        public void Operator_Subtract_DecreasesAmountOnData(IResource resource, int inputAmount, int operatorAmount, int outputAmount)
        {
            modelReference = new ResourceData(resource, inputAmount);

            var result = modelReference - operatorAmount;

            Assert.AreEqual(outputAmount, result.Amount);
        }

        public static IEnumerable Operator_Multiply_UseCases()
        {
            var positiveValue = Random.Range(0,10000);
            var negativeValue = -positiveValue-1;
            var quantifiableResource = Mock.IResource(true);
            var uniqueResource = Mock.IResource(false);

            yield return new object[4]{ quantifiableResource, positiveValue, positiveValue, positiveValue*positiveValue };
            yield return new object[4]{ quantifiableResource, positiveValue, negativeValue, 0 };
            yield return new object[4]{ quantifiableResource, negativeValue, positiveValue, 0 };
            yield return new object[4]{ uniqueResource, positiveValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, positiveValue, negativeValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, negativeValue, 1 };
        }
        [TestCaseSource(nameof(Operator_Multiply_UseCases))]
        public void Operator_Multiply_MultipliesAmountOnData(IResource resource, int inputAmount, int operatorAmount, int outputAmount)
        {
            modelReference = new ResourceData(resource, inputAmount);

            var result = modelReference * operatorAmount;

            Assert.AreEqual(outputAmount, result.Amount);
        }

        public static IEnumerable Operator_Divide_UseCases()
        {
            var positiveValue = Random.Range(0,10000);
            var negativeValue = -positiveValue-1;
            var quantifiableResource = Mock.IResource(true);
            var uniqueResource = Mock.IResource(false);

            yield return new object[4]{ quantifiableResource, positiveValue, positiveValue, positiveValue*positiveValue };
            yield return new object[4]{ quantifiableResource, positiveValue, negativeValue, 0 };
            yield return new object[4]{ quantifiableResource, negativeValue, positiveValue, 0 };
            yield return new object[4]{ uniqueResource, positiveValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, positiveValue, negativeValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, positiveValue, 1 };
            yield return new object[4]{ uniqueResource, negativeValue, negativeValue, 1 };
        }
        [TestCaseSource(nameof(Operator_Divide_UseCases))]
        public void Operator_Divide_DividesAmountOnData(IResource resource, int inputAmount, int operatorAmount, int outputAmount)
        {
            modelReference = new ResourceData(resource, inputAmount);

            var result = modelReference * operatorAmount;

            Assert.AreEqual(outputAmount, result.Amount);
        }
        #endregion
    }
}