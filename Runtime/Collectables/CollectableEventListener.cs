// #define ENABLE_INTERFACE_FIELDS
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Component responsible for listening for changes on a
  Collectable</summary>
  */
  [AddComponentMenu("Trinkets/Collectable/Collectable Event Listener")]
  public class CollectableEventListener
      : EngineBehaviour,
        IResourceProcessor<ICollectableWallet, bool> {
#region Constants
    /**
    <summary>The amount of time (in seconds) between updates.</summary>
    */
    public const float UpdateTime = 0.5f;
#endregion
#region Variables
    /**
    <summary>The collectable to be checked for.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICollectable Collectable {
      get => collectable.Unwrap();
      set => collectable.Set(value);
    }
    /**
    <inheritdoc cref="CollectableEventListener.Collectable"/>
    */
    [SerializeField] Field<ICollectable> collectable =
        new Field<ICollectable>();
#else
    [field:SerializeField]
    public CollectableData Collectable { get; set; }
#endif
    /**
    <summary>The wallet that will be checked by the listener.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICollectableWallet Wallet {
      get => wallet.Unwrap();
      set => wallet.Set(value);
    }
    /**
    <inheritdoc cref="CollectableEventListener.Wallet"/>
    */
    [SerializeField] Field<ICollectableWallet> wallet =
        new Field<ICollectableWallet>();
#else
    [field:SerializeField]
    public CollectableWallet Wallet;
#endif
#endregion
#region Delegates
    /**
    <summary>Delegate to describe a listening event.</summary>
    <param name="wasCollected">Was the collectable in the wallet?</param>
    */
    public delegate void Event(bool wasCollected);
#endregion
#region Events
    /**
    <remarks>Meant as a event gateway for designers to use in the inspector.
    </remarks>
    <inheritdoc cref="CollectableEventListener.onCollectableChange"/>
    */
    [SerializeField]
    UnityEvent<bool> collectableChanged;
    /**
    <summary>Event invoked when the component checks if a collectable is present
    in a wallet. If either the wallet or collectable are not set, the event
    always returns false.</summary>
    <remarks>Meant as a event gateway for programmers to listen for events
    </remarks>
    */
    public event Event onCollectableChange;
#endregion
#region EngineBehaviour Implementation
    /**
    <inheritdoc />
    */
    public override void Setup() { onCollectableChange += OnCollectableChange; }
    /**
    <inheritdoc />
    */
    public override void TearDown() {
      onCollectableChange -= OnCollectableChange;
    }
#endregion
#region IResourceProcessor Implementation
    /**
    <returns><c>true</c> when the collectable is in the wallet.<br/>
    <c>false</c> when the collectable is not in the wallet.</returns>
    <inheritdoc />
    */
    public bool Convert(ICollectableWallet wallet) {
      if (wallet == null || Collectable == null)
        return false;

      return wallet.Contains(Collectable);
    }
#endregion
#region Methods
    /**
    <summary>Coroutine responsible for updating the state of the component.
    </summary>
    */
    private IEnumerator Start() {
      while (true) {
        var wasCollected = Convert(Wallet);
        onCollectableChange?.Invoke(wasCollected);

        yield return new WaitForSeconds(UpdateTime);
      }
    }
    /**
    <summary>Method that invokes the <c>collectableChanged</c> event.</summary>
    <param name="wasCollected">Is the collectable part of the wallet?</param>
    <remarks>Used to subscribe the UnityEvent to the C# version of the
    event.</remarks>
    */
    private void OnCollectableChange(bool wasCollected) {
      collectableChanged?.Invoke(wasCollected);
    }
#endregion
  }
}
