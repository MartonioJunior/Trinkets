using System.Collections.Generic;
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
            if (collectables == null) return true;

            foreach(var field in collectables) {
                if (!field.HasValue()) continue;

                var collectable = field.Unwrap();
                if (!wallet.Contains(collectable)) return false;
            }

            return true;
        }

        public override bool PerformTax(ICollectableWallet wallet)
        {
            if (collectables == null || collectables.Length == 0) return false;

            foreach(var field in collectables) {
                if (!field.HasValue()) continue;

                var collectable = field.Unwrap();
                wallet.Remove(collectable);
            }

            return true;
        }
        #endregion
        #region Methods
        public void SetCriteria(params ICollectable[] requirements)
        {
            int size = requirements?.Length ?? 0;

            var result = new Field<ICollectable>[size];

            for(int i = 0; i < size; i++) {
                result[i] = new Field<ICollectable>(requirements[i]);
            }

            collectables = result;
        }
        #endregion
    }
}