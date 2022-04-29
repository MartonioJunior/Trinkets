using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Currency
{
    [AddComponentMenu("Collectables/Currency/Currency Scanner")]
    public class CurrencyScannerComponent: EngineBehaviour, IResourceScanner<ICurrencyWallet>
    {
        #region Variables
        [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();
        [SerializeField, Min(0f)] int amount;
        [SerializeField] bool taxWallet;
        public ICurrency Currency {
            get => currency.Unpack();
            set => currency.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0, value);
        }
        #endregion
        #region Delegates
        public delegate void ScanEvent(bool scanWasSuccessful);
        public delegate void TaxEvent();
        #endregion
        #region Events
        [SerializeField] UnityEvent<bool> scannedWallet;
        [SerializeField] UnityEvent taxedWallet;
        public event ScanEvent onScanWallet;
        public event TaxEvent onTaxWallet;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IResourceScanner Implementation
        public bool TaxWalletOnScan {
            get => taxWallet;
            set => taxWallet = value;
        }

        public bool Check(ICurrencyWallet wallet)
        {
            return enabled && Mathf.Abs(amount) <= wallet.AmountOf(Currency);
        }

        public void Tax(ICurrencyWallet wallet)
        {
            if (amount > 0) {
                wallet.Change(Currency, -amount);
                onTaxWallet?.Invoke();
            }
        }
        #endregion
        #region Methods
        public void Scan(ICurrencyWallet wallet)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
            onScanWallet?.Invoke(scanResult);
        }

        public void ScanWallet(CurrencyWallet wallet)
        {
            Scan(wallet);
        }

        public void TaxWallet(CurrencyWallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}