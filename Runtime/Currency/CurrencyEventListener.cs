using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Currency
{
    [AddComponentMenu("Collectables/Currency/Currency Event Listener")]
    public class CurrencyEventListener: EngineBehaviour, IResourceProcessor<ICurrencyWallet, int>
    {
        #region Constants
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        [SerializeField] Field<ICurrencyWallet> wallet = new Field<ICurrencyWallet>();
        [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();

        public ICurrency Currency {
            get => currency.Unwrap();
            set => currency.Set(value);
        }

        public ICurrencyWallet Wallet {
            get => wallet.Unwrap();
            set => wallet.Set(value);
        }
        #endregion
        #region Delegate
        public delegate void Change(int amount);
        #endregion
        #region Events
        [SerializeField] UnityEvent<int> amountChanged;
        public event Change onAmountChange;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}

        public override void Setup()
        {
            onAmountChange += OnAmountChange;
        }

        public override void TearDown()
        {
            onAmountChange -= OnAmountChange;
        }
        public override void Validate() {}
        #endregion
        #region IResourceProcessor Implementation
        public int Convert(ICurrencyWallet wallet)
        {
            return wallet?.AmountOf(Currency) ?? -1;
        }
        #endregion
        #region Methods
        private IEnumerator Start()
        {
            while (true) {
                var amount = Convert(Wallet);
                onAmountChange?.Invoke(amount);

                yield return new WaitForSeconds(UpdateTime);
            }
        }

        private void OnAmountChange(int newAmount)
        {
            amountChanged?.Invoke(newAmount);
        }
        #endregion
    }
}