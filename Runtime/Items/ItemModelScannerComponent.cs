using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    [AddComponentMenu("Trinkets/Item/Item Model Scanner")]
    public class ItemModelScannerComponent: ItemScanner
    {
        #region Variables
        [SerializeField] Field<IItemModel> model = new Field<IItemModel>();
        [SerializeField, Min(0)] int amount;

        public IItemModel Model {
            get => model.Unwrap();
            set => model.Set(value);
        }

        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0, value);
        }
        #endregion
        #region ItemScanner Implementation
        public override bool FulfillsCriteria(IItemWallet wallet)
        {
            return wallet.AmountOf(Model) >= amount;
        }

        public override bool PerformTax(IItemWallet wallet)
        {
            if (amount > 0) {
                wallet.Remove(Model, amount);
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }   
}