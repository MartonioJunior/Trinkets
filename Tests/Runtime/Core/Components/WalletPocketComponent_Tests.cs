using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;

namespace Tests.MartonioJunior.Trinkets
{
    public class WalletPocketComponent_Tests: ComponentTestModel<WalletPocketComponent>
    {
        #region TestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        public static IEnumerable UseCases_Wallet()
        {
            var wallet = Substitute.For<Wallet>();

            yield return new object[]{ wallet };
            yield return new object[]{ null };
        }
        [TestCaseSource(nameof(UseCases_Wallet))]
        public void Wallet_ReturnsObjectStoredOnComponent(Wallet data)
        {
            modelReference.Wallet = data;

            Assert.AreEqual(data, modelReference.Wallet);
        }
        #endregion
    }
}