using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Currency
{
    [AddComponentMenu("Collectables/Currency/Currency Giver")]
    public class CurrencyComponent: EngineBehaviour, IResourceInstancer<ICurrencyWallet>
    {
        #region Variables
        [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();
        [SerializeField, Min(0)] int amount;
        public ICurrency Currency {
            get => currency.Unwrap();
            set => currency.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0,value);
        }
        #endregion
        #region Delegates
        public delegate void Event();
        #endregion
        #region Events
        [SerializeField] UnityEvent collectedCurrency;
        public event Event onCollectedCurrency;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}

        public override void Setup()
        {
            onCollectedCurrency += OnCollectedCurrency;
        }

        public override void TearDown()
        {
            onCollectedCurrency -= OnCollectedCurrency;
        }

        public override void Validate() {}
        #endregion
        #region IResourceInstancer Implementation
        public void AddTo(ICurrencyWallet wallet)
        {
            if (enabled) {
                wallet.Change(Currency, amount);
                onCollectedCurrency?.Invoke();
            }
        }
        #endregion
        #region Methods
        public void AddToWallet(CurrencyWallet wallet)
        {
            AddTo(wallet);
        }

        private void OnCollectedCurrency()
        {
            collectedCurrency?.Invoke();
        }
        #endregion
    }
}