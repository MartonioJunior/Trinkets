using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    public class ItemTypeScannerComponent: ItemScanner
    {
        #region Variables
        [SerializeField] Field<IItem> item = new Field<IItem>();
        [SerializeField, Min(0)] int amount;

        public IItem Item {
            get => item.Unwrap();
            set => item.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0, value);
        }
        #endregion
        #region ItemScanner Implementation
        public override bool FulfillsCriteria(IItemWallet wallet)
        {
            return wallet.AmountOf(Item) >= amount;
        }

        public override bool PerformTax(IItemWallet wallet)
        {
            if (amount > 0) {
                for(int i = 0; i < amount; i++) {
                    wallet.Remove(Item);
                }
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }   
}