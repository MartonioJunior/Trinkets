using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Component used as the basis for scanners of collectable wallets.</summary>
    */
    [Obsolete("Functionality replaced by the ResourceScannerComponent")]
    public abstract class CollectableScanner: EngineBehaviour, IResourceScanner
    {
        #region Variables
        /**
        <inheritdoc cref="CollectableScanner.TaxGroupOnScan"/>
        */
        [SerializeField] bool taxWallet;
        #endregion
        #region Delegates
        /**
        <summary>Delegate used to describe a scanning event.</summary>
        <param name="scanResult">The result of the scan operation:<br/>
        <c>true</c> when the wallet fulfills the criteria.<br/>
        <c>false</c> when the scan fails the criteria.</param>
        */
        public delegate void ScanEvent(bool scanResult);
        /**
        <summary>Delegate used to describe a tax event.</summary>
        */
        public delegate void TaxEvent();
        #endregion
        #region Events
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="CollectableScanner.onScanWallet"/>
        */
        [SerializeField] UnityEvent<bool> scannedWallet;
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="CollectableScanner.onTaxWallet"/>
        */
        [SerializeField] UnityEvent taxedWallet;
        /**
        <summary>Event invoked when the component scans a wallet.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event ScanEvent onScanWallet;
        /**
        <summary>Event invoked when the component removes elements from a wallet.
        </summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event TaxEvent onTaxWallet;
        #endregion
        #region Abstract Implementation
        /**
        <summary>Checks whether the wallet fulfills the criteria specified by
        the component.</summary>
        <param name="wallet">The wallet to be checked.</param>
        <returns><c>true</c> when the wallet fulfills the criteria.<br/>
        <c>false</c> when the wallet fails the criteria.</returns>
        */
        public abstract bool FulfillsCriteria(IResourceGroup wallet);
        /**
        <summary>Removes elements from the wallet.</summary>
        <param name="wallet">The wallet to remove elements from</param>
        <returns><c>true</c> when the operation is successful.<br/>
        <c>false</c> when the operation fails.</returns>
        */
        public abstract bool PerformTax(IResourceGroup wallet);
        #endregion
        #region EngineBehaviour Implementation
        /**
        <inheritdoc />
        */
        public override void Setup()
        {
            onScanWallet += OnScanWallet;
            onTaxWallet += OnTaxWallet;
        }
        /**
        <inheritdoc />
        */
        public override void TearDown()
        {
            onScanWallet -= OnScanWallet;
            onTaxWallet -= OnTaxWallet;
        }
        #endregion
        #region IResourceScanner Implementation
        /**
        <inheritdoc />
        */
        public bool TaxGroupOnScan {
            get => taxWallet;
            set => taxWallet = value;
        }
        /**
        <inheritdoc />
        */
        public bool Check(IResourceGroup wallet)
        {
            return enabled && FulfillsCriteria(wallet);
        }
        /**
        <inheritdoc />
        */
        public void Tax(IResourceGroup wallet)
        {
            if (PerformTax(wallet)) {
                onTaxWallet?.Invoke();
            }
        }
        #endregion
        #region Methods
        /**
        <summary>Method used to invoke the <c>scannedWallet</c> event.</summary>
        <param name="scanResult">Was the scan successful?</param>
        */
        private void OnScanWallet(bool scanResult)
        {
            scannedWallet?.Invoke(scanResult);
        }
        /**
        <summary>Method used to invoke the <c>taxedWallet</c> event.</summary>
        */
        private void OnTaxWallet()
        {
            taxedWallet?.Invoke();
        }
        /**
        <summary>Scans a specified wallet.</summary>
        <param name="wallet">The wallet to be scanned.</param>
        */
        public void Scan(ICollectableWallet wallet)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
            onScanWallet?.Invoke(scanResult);
        }
        /**
        <remarks>Works the same as the Scan method, but uses a 
        <c>CollectableWallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        <inheritdoc cref="CollectableScanner.Scan(ICollectableWallet)"/>
        */
        public void ScanWallet(CollectableWallet wallet)
        {
            Scan(wallet);
        }

        /**
        <remarks>Works the same as the Tax method, but uses a 
        <c>CollectableWallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        <inheritdoc cref="CollectableScanner.Tax(ICollectableWallet)"/>
        */
        public void TaxWallet(CollectableWallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}