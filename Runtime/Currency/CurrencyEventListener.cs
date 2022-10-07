// #define ENABLE_INTERFACE_FIELDS
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Currencies
{
    [Obsolete("Functionality replaced by the WalletListenerComponent")]
    [AddComponentMenu("Trinkets/Currency/Currency Event Listener")]
    public class CurrencyEventListener: EngineBehaviour
    {
        #region Constants
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        /**
        <summary>The type of currency to be listened out by the component.</summary>
        */
        #if ENABLE_INTERFACE_FIELDS
        public ICurrency Currency {
            get => currency.Unwrap();
            set => currency.Set(value);
        }
        /**
        <inheritdoc cref="CurrencyEventListener.Currency"/>
        */
        [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();
        #else
        [field: SerializeField] public CurrencyData Currency {get; set;}
        #endif
        /**
        <summary>The wallet that will be checked by the listener.</summary>
        */
        #if ENABLE_INTERFACE_FIELDS
        public ICurrencyWallet Wallet {
            get => wallet.Unwrap();
            set => wallet.Set(value);
        }
        /**
        <inheritdoc cref="CurrencyEventListener.Wallet"/>
        */
        [SerializeField] Field<ICurrencyWallet> wallet = new Field<ICurrencyWallet>();
        #else
        [field: SerializeField] public CurrencyWallet Wallet {get; set;}
        #endif
        #endregion
        #region Delegate
        /**
        <summary>Delegate to describe a listening event.</summary>
        <param name="amount">The amount of the specified currency in the wallet.</param>
        */
        public delegate void Change(int amount);
        #endregion
        #region Events
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="CurrencyEventListener.onAmountChange"/>
        */
        [SerializeField] UnityEvent<int> amountChanged;
        /**
        <summary>Event invoked when the component checks the amount of currency
        present in a wallet. If either the wallet or collectable are not set,
        the event always returns zero.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events
        </remarks>
        */
        public event Change onAmountChange;
        #endregion
        #region EngineScrob Implementation
        /**
        <inheritdoc />
        */
        public override void Setup()
        {
            onAmountChange += OnAmountChange;
        }
        /**
        <inheritdoc />
        */
        public override void TearDown()
        {
            onAmountChange -= OnAmountChange;
        }
        #endregion
        #region IResourceProcessor Implementation
        /**
        <inheritdoc />
        */
        public int Convert(ICurrencyWallet wallet)
        {
            return wallet?.AmountOf(Currency) ?? -1;
        }
        #endregion
        #region Methods
        /**
        <summary>Coroutine responsible for updating the state of the component.
        </summary>
        */
        private IEnumerator Start()
        {
            while (true) {
                var amount = Convert(Wallet);
                onAmountChange?.Invoke(amount);

                yield return new WaitForSeconds(UpdateTime);
            }
        }
        /**
        <summary>Method that invokes the <c>amountChanged</c> event.</summary>
        <param name="newAmount">What's the amount in the wallet?</param>
        <remarks>Used to subscribe the UnityEvent to the C# version of the event.</remarks>
        */
        private void OnAmountChange(int newAmount)
        {
            amountChanged?.Invoke(newAmount);
        }
        #endregion
    }
}