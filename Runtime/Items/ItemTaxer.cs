using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Items
{
    [AddComponentMenu("Collectables/Item Taxer")]
    public class ItemTaxer: EngineBehaviour, IResourceTaxer<ItemWallet>
    {
        #region Variables
        [SerializeField] Func<IItem, IItem, bool> comparer;
        [SerializeField] Field<IItem> reference;
        [SerializeField] int amount;
        #endregion
        #region Events
        [SerializeField] UnityEvent paidTax;
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IResourceTaxer Implementation
        public bool CanBeTaxed(ItemWallet wallet)
        {
            throw new NotImplementedException();
        }

        public void Tax(ItemWallet wallet)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}