using UnityEngine;

namespace MartonioJunior.Collectables.Collectables
{
    [AddComponentMenu("Collectables/Collectable/Collectable Scanner")]
    public class CollectableCategoryScannerComponent: CollectableScanner
    {
        #region Variables
        [SerializeField] Field<ICollectableCategory> category;
        [SerializeField] int amount;
        #endregion
        #region ScannerComponent Implementation
        public override bool FulfillsCriteria(ICollectableWallet wallet)
        {
            if (!category.HasValue()) return false;

            return wallet.AmountOf(category.Unpack()) >= amount;
        }

        public override bool PerformTax(ICollectableWallet wallet)
        {
            if (amount > 0 && category.HasValue()) {
                wallet.Remove(category.Unpack());
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}