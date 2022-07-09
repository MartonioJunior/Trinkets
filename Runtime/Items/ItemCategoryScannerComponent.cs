using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    [AddComponentMenu("Trinkets/Item/Item Category Scanner")]
    public class ItemCategoryScannerComponent: ItemScanner
    {
        #region Variables
        [SerializeField] Field<IItemCategory> category = new Field<IItemCategory>();
        [SerializeField, Min(0)] int amount;

        public IItemCategory Category {
            get => category.Unwrap();
            set => category.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0, value);
        }
        #endregion
        #region ItemScanner Implementation
        public override bool FulfillsCriteria(IItemWallet wallet)
        {
            return category.HasValue() && wallet.AmountOf(Category) >= amount;
        }

        public override bool PerformTax(IItemWallet wallet)
        {
            if (amount > 0) {
                wallet.Remove(Category, amount);
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }   
}