// #define ENABLE_INTERFACE_FIELDS
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Component that scans a wallet for collectables that belong to a
  specified category.</summary>
  */
  [AddComponentMenu("Trinkets/Collectable/Collectable Category Scanner")]
  public class CollectableCategoryScannerComponent : CollectableScanner {
#region Variables
    /**
    <summary>The category used as the scan criteria.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    public ICollectableCategory Category {
      get => category.Unwrap();
      set => category.Set(value);
    }
    /**
    <inheritdoc cref="CollectableCategoryScannerComponent.Category" />
    */
    [SerializeField] Field<ICollectableCategory> category =
        new Field<ICollectableCategory>();
#else
    [field:SerializeField]
    public CollectableCategory Category { get; set; }
#endif
    /**
    <summary>The amount of collectables required for a scan to pass.</summary>
    */
    [field:SerializeField, Min(0f)]
    public int Amount { get; set; }
#endregion
#region ScannerComponent Implementation
    /**
    <inheritdoc />
    */
    public override bool FulfillsCriteria(ICollectableWallet wallet) {
#if ENABLE_INTERFACE_FIELDS
      if (category.Get(out var validCategory))
#else
      if (Category is CollectableCategory validCategory)
#endif
        return wallet.AmountOf(validCategory) >= Amount;
      else
        return false;
    }
    /**
    <inheritdoc />
    */
    public override bool PerformTax(ICollectableWallet wallet) {
#if ENABLE_INTERFACE_FIELDS
      if (!category.Get(out var validCategory))
#else
      if (!(Category is CollectableCategory validCategory))
#endif
        return false;

      wallet.Remove(validCategory, Amount);
      return Amount > 0;
    }
#endregion
  }
}