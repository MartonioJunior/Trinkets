using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Collectables
{
    public abstract class CollectableScanner: EngineBehaviour, IResourceScanner<ICollectableWallet>
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
        #region Abstract Implementation
        public abstract bool FulfillsCriteria(ICollectableWallet wallet);
        public abstract bool PerformTax(ICollectableWallet wallet);
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

        public bool Check(ICollectableWallet wallet)
        {
            return enabled && FulfillsCriteria(wallet);
        }

        public void Tax(ICollectableWallet wallet)
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

        public void Scan(ICollectableWallet wallet)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
            onScanWallet?.Invoke(scanResult);
        }

        public void ScanWallet(CollectableWallet wallet)
        {
            Scan(wallet);
        }

        public void TaxWallet(CollectableWallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}