// #define ENABLE_INTERFACE_FIELDS
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Currency
{
/**
<summary>Component that gives currency to a wallet.</summary>
*/
[AddComponentMenu("Trinkets/Currency/Currency Giver")]
public class CurrencyComponent: EngineBehaviour, IResourceInstancer<ICurrencyWallet>
{
    #region Variables
    /**
    <summary>The type of currency to be given out by the component.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICurrency Currency {
        get => currency.Unwrap();
        set => currency.Set(value);
    }
    /**
    <inheritdoc cref="CurrencyComponent.Currency"/>
    */
    [SerializeField] Field<ICurrency> currency = new Field<ICurrency>();
#else
    [field: SerializeField] public CurrencyData Currency {
        get;
        set;
    }
#endif
    /**
    <summary>The amount to be added into a wallet.</summary>
    <remarks>This value never returns a value lower than zero.</remarks>
    */
    public int Amount {
        get => amount;
        set => amount = Mathf.Max(0,value);
    }
    /**
    <inheritdoc cref="CurrencyComponent.Amount"/>
    */
    [SerializeField, Min(0)] int amount;
    #endregion
    #region Delegates
    /**
    <summary>Delegate used to describe a <c>CurrencyComponent</c> component.
    </summary>
    */
    public delegate void Event();
    #endregion
    #region Events
    /**
    <remarks>Meant as a event gateway for designers to use in the inspector.
    </remarks>
    <inheritdoc cref="CurrencyComponent.onCollected"/>
    */
    [SerializeField] UnityEvent collectedCurrency;
    /**
    <summary>Event invoked when the component attempts to add currency to a
    wallet.</summary>
    <remarks>Meant as a event gateway for programmers to listen for events.
    </remarks>
    */
    public event Event onCollectedCurrency;
    #endregion
    #region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup()
    {
        onCollectedCurrency += OnCollectedCurrency;
    }
    /**
    <inheritdoc />
    */
    public override void TearDown()
    {
        onCollectedCurrency -= OnCollectedCurrency;
    }
    #endregion
    #region IResourceInstancer Implementation
    /**
    <summary>Adds the specified amount of currency into the wallet.</summary>
    <inheritdoc />
    */
    public void AddTo(ICurrencyWallet wallet)
    {
        if (enabled) {
            wallet.Change(Currency, amount);
            onCollectedCurrency?.Invoke();
        }
    }
    #endregion
    #region Methods
    /**
    <remarks>This method works the same as AddTo, but receives a
    <c>CurrencyWallet</c> instead to allow for use with events
    in the Unity inspector.</remarks>
    <inheritdoc cref="CurrencyComponent.AddTo(ICurrencyWallet)"/>
    */
    public void AddToWallet(CurrencyWallet wallet)
    {
        AddTo(wallet);
    }
    /**
    <summary>Method that invokes the <c>collectedEvent</c> event.</summary>
    <remarks>Used to subscribe the UnityEvent to the C# version of the event.</remarks>
    */
    private void OnCollectedCurrency()
    {
        collectedCurrency?.Invoke();
    }
    #endregion
}
}