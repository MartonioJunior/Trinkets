using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Currency
{
    [AddComponentMenu("Collectables/Currency/Currency Event Listener")]
    public class CurrencyEventListener: EngineBehaviour, IResourceProcessor<ICurrencyWallet, int>
    {
        #region Variables
        [SerializeField] Field<ICurrencyWallet> wallet = new Field<ICurrencyWallet>();
        [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();

        public ICurrency Currency {
            get => currency.Unpack();
            set => currency.Set(value);
        }

        public ICurrencyWallet Wallet {
            get => wallet.Unpack();
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
            FixedUpdate();
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
        private void FixedUpdate()
        {
            var amount = Convert(Wallet);
            onAmountChange?.Invoke(amount);
        }

        private void OnAmountChange(int newAmount)
        {
            amountChanged?.Invoke(newAmount);
        }
        #endregion
    }
}