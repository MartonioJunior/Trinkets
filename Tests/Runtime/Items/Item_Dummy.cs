using MartonioJunior.Trinkets.Items;
using UnityEngine;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class Item_Dummy: IItem
    {
        #region Variables
        ItemModel_Dummy dummy;
        #endregion
        #region Constructors
        public Item_Dummy(ItemModel_Dummy model)
        {
            dummy = model;
            Value = 1;
        }
        #endregion
        #region IItem Implementation
        public int Value {get; set;}
        public string Name => dummy.Name;
        public Sprite Image {get; set;}
        public IItemModel Model => dummy;

        public void AddTo(IItemWallet wallet)
        {
            Model.AddTo(wallet);
        }

        public IItem Copy()
        {
            var newItem = new Item_Dummy(dummy);
            newItem.Value = this.Value+1;
            newItem.Image = this.Image;
            return newItem;
        }
        #endregion
    }
}