using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    [AddComponentMenu("Trinkets/Collectable/Collectable Scanner")]
    public class CollectableCategoryScannerComponent: CollectableScanner
    {
        #region Variables
        [SerializeField] Field<ICollectableCategory> category = new Field<ICollectableCategory>();
        [SerializeField] int amount;

        public ICollectableCategory Category {
            get => category.Unwrap();
            set => category.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = value;
        }
        #endregion
        #region ScannerComponent Implementation
        public override bool FulfillsCriteria(ICollectableWallet wallet)
        {
            if (!category.HasValue()) return false;

            return wallet.AmountOf(category.Unwrap()) >= amount;
        }

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