using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Collectables;
using NSubstitute;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets.Collectables
{
    public class ICollectableWallet_Tests: TestModel<ICollectableWallet>
    {
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Substitute.For<ICollectableWallet>();
        }

        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_With()
        {
            yield return new object[]{Substitute.For<ICollectable>(), 1};
            yield return new object[]{null, 0};
        }
        [TestCaseSource(nameof(UseCases_With))]
        public void With_ReturnsWalletWithAddedCollectables(ICollectable collectable, int resultAmount)
        {
            modelReference.Add(Arg.Any<IResourceData>()).Returns(true);
            modelReference.AmountOf(Arg.Any<ICollectable>()).Returns(resultAmount);

            Assert.AreEqual(modelReference, modelReference.With(collectable));
            Assert.AreEqual(resultAmount, modelReference.AmountOf(collectable));
        }
        #endregion
    }
}