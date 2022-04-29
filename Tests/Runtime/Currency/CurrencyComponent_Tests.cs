using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Currency;
using UnityEngine.Events;
using System;

namespace Tests.MartonioJunior.Collectables.Currency
{
    public class CurrencyComponent_Tests: ComponentTestModel<CurrencyComponent>
    {
        #region Constants
        public const int AmountIncreased = 80;
        public const int NegativeAmount = -2;
        #endregion
        #region Variables
        private ICurrency Currency;
        private CurrencyWallet Wallet;
        #endregion
        #region ComponentTestModel Implementation
        public override void CreateTestContext()
        {
            Wallet = ScriptableObject.CreateInstance<CurrencyWallet>();
            Currency = ScriptableObject.CreateInstance<CurrencyData>();
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Currency = Currency;
            modelReference.Amount = AmountIncreased;
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
        #region Methods
        [Test]
        public void AddTo_InsertsResourceIntoWallet()
        {
            modelReference.AddTo(Wallet);

            Assert.AreEqual(AmountIncreased, Wallet.AmountOf(Currency));
        }

        [Test]
        public void AddTo_DoesNothingWhenComponentIsDisabled()
        {
            modelReference.enabled = false;
            modelReference.AddTo(Wallet);

            Assert.Zero(Wallet.AmountOf(Currency));
        }

        [Test]
        public void AddTo_RaisesEventWhenCollected()
        {
            int triggerCount = 0;
            modelReference.onCollectedCurrency += () => triggerCount++;

            Assert.Zero(triggerCount);
            modelReference.AddTo(Wallet);
            Assert.AreEqual(1, triggerCount);
        }

        [Test]
        public void AddToWallet_WorksLikeAddToMethod()
        {
            modelReference.AddToWallet(Wallet);

            Assert.AreEqual(AmountIncreased, Wallet.AmountOf(Currency));
        }

        [Test]
        public void Amount_CannotBeNegative()
        {
            modelReference.Amount = NegativeAmount;

            Assert.False(modelReference.Amount < 0);
        }
        #endregion
    }
}