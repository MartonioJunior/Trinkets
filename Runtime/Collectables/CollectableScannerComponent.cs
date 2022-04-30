using UnityEngine;

namespace MartonioJunior.Collectables.Collectables
{
    [AddComponentMenu("Collectables/Collectable/Collectable Scanner")]
    public class CollectableScannerComponent: CollectableScanner
    {
        #region Variables
        [SerializeField] Field<ICollectable>[] collectables;
        #endregion
        #region Collectable Scanner Implementation
        public override bool FulfillsCriteria(ICollectableWallet wallet)
        {
            foreach(var field in collectables) {
                if (!field.HasValue()) continue;

                var collectable = field.Unpack();
                if (!wallet.Contains(collectable)) return false;
            }

            return true;
        }

        public override bool PerformTax(ICollectableWallet wallet)
        {
            if (collectables == null || collectables.Length == 0) return false;

            foreach(var field in collectables) {
                if (!field.HasValue()) continue;

                var collectable = field.Unpack();
                wallet.Remove(collectable);
            }

            return true;
        }
        #endregion
    }
}