using System;
using System.Collections;
using MartonioJunior.Collectables.Currency;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Collectables.Currency
{
    public class CurrencyEventListener_Tests: ComponentTestModel<CurrencyEventListener>
    {
        #region Constants
        const int AmountOnWallet = 99;
        #endregion
        #region Variables
        private ICurrency Currency;
        private CurrencyWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            Wallet = ScriptableObject.CreateInstance<CurrencyWallet>();
            Currency = ScriptableObject.CreateInstance<CurrencyData>();
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Currency = Currency;
            modelReference.Wallet = Wallet;
            Wallet.Change(Currency, AmountOnWallet);
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();
            ScriptableObject.DestroyImmediate(Wallet);
            ScriptableObject.DestroyImmediate(Currency as CurrencyData);
            Wallet = null;
            Currency = null;
        }
        #endregion
        #region Method Tests
        [Test]
        public void Convert_ReturnsAmountOfCurrencyInsideWallet()
        {
            Assert.AreEqual(AmountOnWallet, modelReference.Convert(Wallet));
        }

        [Test]
        public void Setup_InvokesFixedUpdateMethod()
        {
            int amountOnWallet = 0;
            modelReference.onAmountChange += (amount) => amountOnWallet = amount;

            Assert.Zero(amountOnWallet);
            modelReference.Setup();
            Assert.AreEqual(AmountOnWallet, amountOnWallet);
        }
        #endregion
        #region Coroutine Tests
        [UnityTest]
        public IEnumerator FixedUpdate_InvokesOnAmountChangeEvent()
        {
            int amountOnWallet = 0;
            modelReference.onAmountChange += (amount) => amountOnWallet = amount;

            Assert.Zero(amountOnWallet);

            yield return null;

            Assert.AreEqual(AmountOnWallet, amountOnWallet);
        }
        #endregion
    }
}