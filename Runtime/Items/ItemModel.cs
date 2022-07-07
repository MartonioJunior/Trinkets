using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    public abstract class ItemModel: EngineScrob, IItemModel
    {
        #region Variables
        [SerializeField] Field<IItemCategory> category = new Field<IItemCategory>();
        #endregion
        #region Abstract Implementation
        public abstract void AddTo(IItemWallet wallet);
        #endregion
        #region IItemModel Implementation
        #endregion
        public IItemCategory Category {
            get => category.Unwrap();
            set {
                category.Set(value);
            }
        }

        public IItemModel Model => this;
    }
}