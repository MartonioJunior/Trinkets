// #define ENABLE_INTERFACE_FIELDS
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Component that scans a wallet for collectables inside of
  it.</summary>
  */
  [AddComponentMenu("Trinkets/Collectable/Collectable Scanner")]
  [System.Serializable]
  public class CollectableScannerComponent : CollectableScanner {
#region Variables
    /**
    <summary>List of collectables to be checked for.</summary>
    */
#if ENABLE_INTERFACE_FIELDS
    [SerializeField, HideInInspector]
    Field<ICollectable>[] collectables;
#else
    [field:SerializeField]
    public List<CollectableData> Collectables { get; private set; }
#endif
#endregion
#region Collectable Scanner Implementation
    /**
    <inheritdoc />
    */
    public override bool FulfillsCriteria(ICollectableWallet wallet) {
      bool criteriaIsFulfilled = true;
#if !ENABLE_INTERFACE_FIELDS
      var collectables = Collectables;
#endif
      foreach (var field in collectables) {
#if ENABLE_INTERFACE_FIELDS
        if (!field.Get(out var collectable))
          continue;
#else
        if (!(field is CollectableData collectable))
          continue;
#endif

        if (!wallet.Contains(collectable)) {
          criteriaIsFulfilled = false;
          break;
        }
      }
      return criteriaIsFulfilled;
    }
    /**
    <inheritdoc />
    */
    public override bool PerformTax(ICollectableWallet wallet) {
      int amountToRemove;
#if ENABLE_INTERFACE_FIELDS
      if (collectables == null)
        return false;
      amountToRemove = collectables.Length;

      foreach (var field in collectables) {
        if (!field.HasValue())
          continue;

        var collectable = field.Unwrap();
        wallet.Remove(collectable);
      }
#else
      if (!(Collectables is List<CollectableData> collectableList))
        return false;
      amountToRemove = collectableList.Count;

      collectableList.ForEach((item) => wallet.Remove(item));
#endif
      return amountToRemove > 0;
    }
#endregion
#region Methods
    /**
    <summary>Sets the collectables to be used as the criteria for the
    component.</summary>
    <param name="requirements">List of collectables required.</param>
    */
#if ENABLE_INTERFACE_FIELDS
    public void SetCriteria(params ICollectable[] requirements)
#else
    public void SetCriteria(params CollectableData[] requirements)
#endif
    {
      int size = requirements?.Length ?? 0;

#if ENABLE_INTERFACE_FIELDS
      var result = new Field<ICollectable>[size];
#else
      if (Collectables == null)
        Collectables = new List<CollectableData>();
      Collectables.Clear();
#endif

      for (int i = 0; i < size; i++) {
#if ENABLE_INTERFACE_FIELDS
        result[i] = new Field<ICollectable>(requirements[i]);
#else
        Collectables.Add(requirements[i]);
#endif
      }

#if ENABLE_INTERFACE_FIELDS
      collectables = result;
#endif
    }
#endregion
  }
}
