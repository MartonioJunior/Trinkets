using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Component that scans a wallet for collectables that belong to a
    specified category.</summary>
    */
    [AddComponentMenu("Trinkets/Collectable/Collectable Category Scanner")]
    public class CollectableCategoryScannerComponent: CollectableScanner
    {
        #region Variables
        /**
        <inheritdoc cref="CollectableCategoryScannerComponent.Category" />
        */
        [SerializeField] Field<ICollectableCategory> category = new Field<ICollectableCategory>();
        /**
        <inheritdoc cref="CollectableCategoryScannerComponent.Amount"/>
        */
        [SerializeField] int amount;
        /**
        <summary>The category used as the scan criteria.</summary>
        */
        public ICollectableCategory Category {
            get => category.Unwrap();
            set => category.Set(value);
        }
        /**
        <summary>The amount of collectables required for a scan to pass.</summary>
        */
        public int Amount {
            get => amount;
            set => amount = value;
        }
        #endregion
        #region ScannerComponent Implementation
        /**
        <inheritdoc />
        */
        public override bool FulfillsCriteria(ICollectableWallet wallet)
        {
            if (!category.HasValue()) return false;

            return wallet.AmountOf(category.Unwrap()) >= amount;
        }
        /**
        <inheritdoc />
        */
        public override bool PerformTax(ICollectableWallet wallet)
        {
            if (amount > 0 && category.HasValue()) {
                wallet.Remove(category.Unwrap());
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}