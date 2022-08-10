// #define ENABLE_INTERFACE_FIELDS
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Currency {
  /**
  <summary>Component able to scan currencies inside a wallet.</summary>
  */
  [AddComponentMenu("Trinkets/Currency/Currency Scanner")]
  public class CurrencyScannerComponent : EngineBehaviour,
                                          IResourceScanner<ICurrencyWallet> {
#region Variables
    /**
    <summary>The currency to be checked.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICurrency Currency {
      get => currency.Unwrap();
      set => currency.Set(value);
    }
    /**
    <inheritdoc cref="CurrencyScannerComponent.Currency"/>
    */
    [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();
#else
    [field:SerializeField]
    public CurrencyData Currency { get; set; }
#endif
    /**
    <inheritdoc cref="CurrencyScannerComponent.Amount"/>
    */
    [SerializeField, Min(0f)]
    int amount;
    /**
    <inheritdoc cref="IResourceScanner{T}.TaxWalletOnScan"/>
    */
    [SerializeField]
    bool taxWallet;
    /**
    <summary>The amount of currency required for a scan to be
    successful.</summary>
    */
    public int Amount {
      get => amount;
      set => amount = Mathf.Max(0, value);
    }
#endregion
#region Delegates
    /**
    <summary>Delegate used to describe a scanning event.</summary>
    <param name="scanResult">The result of the scan operation:<br/>
    <c>true</c> when the wallet fulfills the criteria.<br/>
    <c>false</c> when the scan fails the criteria.</param>
    */
    public delegate void ScanEvent(bool scanWasSuccessful);
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
    [SerializeField]
    UnityEvent<bool> scannedWallet;
    /**
    <remarks>Meant as a event gateway for designers to use in the inspector.
    </remarks>
    <inheritdoc cref="CollectableScanner.onTaxWallet"/>
    */
    [SerializeField]
    UnityEvent taxedWallet;
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
#region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup() {
      onScanWallet += OnScanWallet;
      onTaxWallet += OnTaxWallet;
    }
    /**
    <inheritdoc />
    */
    public override void TearDown() {
      onScanWallet -= OnScanWallet;
      onTaxWallet -= OnTaxWallet;
    }
#endregion
#region IResourceScanner Implementation
    /**
    <inheritdoc />
    */
    public bool TaxWalletOnScan {
      get => taxWallet;
      set => taxWallet = value;
    }
    /**
    <inheritdoc />
    */
    public bool Check(ICurrencyWallet wallet) {
      return enabled && Mathf.Abs(amount) <= wallet.AmountOf(Currency);
    }
    /**
    <inheritdoc />
    */
    public void Tax(ICurrencyWallet wallet) {
      if (amount > 0) {
        wallet.Change(Currency, -amount);
        onTaxWallet?.Invoke();
      }
    }
#endregion
#region Methods
    /**
    <summary>Method used to invoke the <c>scannedWallet</c> event.</summary>
    <param name="scanResult">Was the scan successful?</param>
    */
    public void OnScanWallet(bool scanResult) {
      scannedWallet?.Invoke(scanResult);
    }
    /**
    <summary>Method used to invoke the <c>taxedWallet</c> event.</summary>
    */
    public void OnTaxWallet() { taxedWallet?.Invoke(); }
    /**
    <summary>Scans a specified wallet.</summary>
    <param name="wallet">The wallet to be scanned.</param>
    */
    public void Scan(ICurrencyWallet wallet) {
      bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
      onScanWallet?.Invoke(scanResult);
    }
    /**
    <remarks>Works the same as the Scan method, but uses a
    <c>CurrencyWallet</c> instead to allow for use with events
    in the Unity inspector.</remarks>
    <inheritdoc cref="CollectableScanner.Scan(ICurrencyWallet)"/>
    */
    public void ScanWallet(CurrencyWallet wallet) { Scan(wallet); }
    /**
    <remarks>Works the same as the Tax method, but uses a
    <c>CurrencyWallet</c> instead to allow for use with events
    in the Unity inspector.</remarks>
    <inheritdoc cref="CollectableScanner.Tax(ICurrencyWallet)"/>
    */
    public void TaxWallet(CurrencyWallet wallet) { Tax(wallet); }
#endregion
  }
}
