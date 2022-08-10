// #define ENABLE_INTERFACE_FIELDS
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
/**
<summary>Component used to give resources to a wallet.</summary>
*/
[AddComponentMenu("Trinkets/Collectable/Collectable Giver")]
public class CollectableComponent: EngineBehaviour, IResourceInstancer<ICollectableWallet>
{
    #region Variables
    /**
    <summary>The collectable to be given out when AddTo is called.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICollectable Collectable {
        get => collectable.Unwrap();
        set => collectable.Set(value);
    }
    /**
    <inheritdoc cref="CollectableComponent.Collectable" />
    */
    [SerializeField] Field<ICollectable> collectable = new Field<ICollectable>();
#else
    [field: SerializeField] public CollectableData Collectable {
        get;
        set;
    }
#endif
    #endregion
    #region Delegate
    /**
    <summary>Delegate to describe a addition event.</summary>
    <param name="newlyAdded">Was the collectable added to the wallet?</param>
    */
    public delegate void Event(bool newlyAdded);
    #endregion
    #region Events
    /**
    <remarks>Meant as a event gateway for designers to use in the inspector.
    </remarks>
    <inheritdoc cref="CollectableComponent.onCollected"/>
    */
    [SerializeField] UnityEvent<bool> collectedEvent;
    /**
    <summary>Event invoked when the component attempts to add a collectable
    to a wallet.</summary>
    <remarks>Meant as a event gateway for programmers to listen for events.
    </remarks>
    */
    public event Event onCollected;
    #endregion
    #region EngineBehaviour Implementation
    /**
    <inheritdoc />
    */
    public override void Setup()
    {
        onCollected += OnCollected;
    }
    /**
    <inheritdoc />
    */
    public override void TearDown()
    {
        onCollected -= OnCollected;
    }
    #endregion
    #region IResourceInstancer Implementation
    /**
    <summary>Add the current set collectable to the wallet.</summary>
    <param name="wallet">The wallet that'll receive the collectable.</param>
    <remarks>The function requires the component to be enabled for the
    collectable to be added.</remarks>
    <inheritdoc />
    */
    public void AddTo(ICollectableWallet wallet)
    {
        if (!enabled) return;
#if ENABLE_INTERFACE_FIELDS
        if (!collectable.Get(out var validCollectable))
#else
        if (!(Collectable is CollectableData validCollectable))
#endif
            return;

        bool newAddition = wallet.Add(Collectable);
        onCollected?.Invoke(newAddition);
    }
    #endregion
    #region Methods
    /**
    <remarks>This method works the same as AddTo, but receives a
    <c>CollectableWallet</c> instead to allow for use with events
    in the Unity inspector.</remarks>
    <inheritdoc cref="CollectableComponent.AddTo(ICollectableWallet)"/>
    */
    public void AddToWallet(CollectableWallet wallet)
    {
        AddTo(wallet);
    }
    /**
    <summary>Method that invokes the <c>collectedEvent</c> event.</summary>
    <param name="newlyAdded">Was the collectable added to the wallet?</param>
    <remarks>Used to subscribe the UnityEvent to the C# version of the event.</remarks>
    */
    private void OnCollected(bool newlyAdded)
    {
        collectedEvent?.Invoke(newlyAdded);
    }
    #endregion
}
}