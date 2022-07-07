using MartonioJunior.Trinkets.Items;
using UnityEngine;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemModel_Dummy : ItemModel
    {
        #region Properties
        public Item_Dummy New => new Item_Dummy(this);
        #endregion
        #region ItemModel Validation
        public override void AddTo(IItemWallet wallet)
        {
            wallet?.Add(New);
        }

        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
    }
}