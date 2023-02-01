using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;
using NSubstitute;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets
{
    public class WalletDetectorComponent_Tests: ComponentTestModel<WalletDetectorComponent>
    {
        #region ComponentTestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Test Preparation
        public static void AttachWalletToGameObject(GameObject obj, Wallet wallet)
        {
            obj.AddComponent<WalletPocketComponent>().Wallet = wallet;
        }
        #endregion
        #region Method Tests
        [Test]
        public void GetWallet_FetchesWalletOnGameObjectAndChildren()
        {
            var gameObject = new GameObject();
            AssertCase(gameObject, false, null);

            var childGameObject = new GameObject();
            childGameObject.transform.SetParent(gameObject.transform);
            AttachWalletToGameObject(childGameObject, Substitute(out Wallet wallet));
            AssertCase(gameObject, true, wallet);

            AttachWalletToGameObject(gameObject, Substitute(out Wallet otherWallet));
            AssertCase(gameObject, true, otherWallet);

            GameObject.DestroyImmediate(gameObject);

            void AssertCase(GameObject gameObject, bool output, Wallet wallet) {
                var operationResult = modelReference.GetWallet(gameObject, out var result);

                Assert.AreEqual(output, operationResult);
                Assert.AreEqual(wallet, result);
            }            
        }
        #endregion
    }
}