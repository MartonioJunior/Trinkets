using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Items
{
    public abstract class ItemScanner: EngineBehaviour, IResourceScanner<IItemWallet>
    {
        #region Variables
        [SerializeField] bool taxWallet;
        #endregion
        #region Delegates
        public delegate void ScanEvent(bool scanResult);
        public delegate void TaxEvent();
        #endregion
        #region Events
        [SerializeField] UnityEvent<bool> scannedWallet;
        [SerializeField] UnityEvent taxedWallet;
        public event ScanEvent onScanWallet;
        public event TaxEvent onTaxWallet;
        #endregion
        #region Abstract Methods
        public abstract bool FulfillsCriteria(IItemWallet wallet);
        public abstract bool PerformTax(IItemWallet wallet);
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}

        public override void Setup()
        {
            onScanWallet += OnScanWallet;
            onTaxWallet += OnTaxWallet;
        }

        public override void TearDown()
        {
            onScanWallet -= OnScanWallet;
            onTaxWallet -= OnTaxWallet;
        }

        public override void Validate() {}
        #endregion
        #region IResourceScanner Implementation
        public bool TaxWalletOnScan {
            get => taxWallet;
            set => taxWallet = value;
        }

        public bool Check(IItemWallet wallet)
        {
            return enabled && FulfillsCriteria(wallet);
        }

        public void Tax(IItemWallet wallet)
        {
            if (PerformTax(wallet)) {
                onTaxWallet?.Invoke();
            }
        }
        #endregion
        #region Methods
        private void OnScanWallet(bool scanResult)
        {
            scannedWallet?.Invoke(scanResult);
        }

        private void OnTaxWallet()
        {
            taxedWallet?.Invoke();
        }

        public void Scan(IItemWallet wallet)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
            onScanWallet?.Invoke(scanResult);
        }

        public void ScanWallet(ItemWallet wallet)
        {
            Scan(wallet);
        }

        public void TaxWallet(ItemWallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}