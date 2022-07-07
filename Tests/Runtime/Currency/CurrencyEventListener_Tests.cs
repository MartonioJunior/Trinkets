using System;
using System.Collections;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currency;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Tests.MartonioJunior.Trinkets.Currency
{
    public class CurrencyEventListener_Tests: ComponentTestModel<CurrencyEventListener>
    {
        #region Constants
        const int AmountOnWallet = 99;
        #endregion
        #region Variables
        private CurrencyData Currency;
        private CurrencyWallet Wallet;
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            EngineScrob.Instance(out Wallet);
            EngineScrob.Instance(out Currency);

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
            ScriptableObject.DestroyImmediate(Currency);

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
        #endregion
        #region Coroutine Tests
        [UnityTest]
        public IEnumerator Start_InvokesOnAmountChangeEvent()
        {
            int amountOnWallet = 0;
            modelReference.onAmountChange += (amount) => amountOnWallet = amount;

            Assert.Zero(amountOnWallet);

            yield return new WaitForSeconds(CurrencyEventListener.UpdateTime);

            Assert.AreEqual(AmountOnWallet, amountOnWallet);
        }

        [UnityTest]
        public IEnumerator Start_UpdatesOnALoopBasedOnUpdateTime()
        {
            int amountOnWallet = Wallet.AmountOf(Currency);
            modelReference.onAmountChange += (amount) => amountOnWallet = amount;
            Wallet.Change(Currency, AmountOnWallet);

            yield return new WaitForSeconds(CurrencyEventListener.UpdateTime-0.05f);

            Assert.AreEqual(AmountOnWallet, amountOnWallet);
            yield return new WaitForSeconds(0.1f);

            const int IncreasedAmount = 2 * AmountOnWallet;
            Assert.AreEqual(IncreasedAmount, amountOnWallet);
        }
        #endregion
    }
}