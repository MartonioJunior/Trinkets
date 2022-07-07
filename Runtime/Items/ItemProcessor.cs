using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    public abstract class ItemProcessor<TItem, TResult>: EngineBehaviour, IResourceProcessor<IItemWallet, TResult> where TItem: class, IItem
    {
        #region Variables
        [SerializeField] Field<IItemModel> model = new Field<IItemModel>();
        [SerializeField] int listenerIndex;
        #endregion
        #region Properties
        public IItemModel Model {
            get => model.Unwrap();
            set {
                model.Set(value);
            }
        }
        #endregion
        #region Abstract Methods
        public abstract TResult Convert(TItem item);
        #endregion
        #region EngineBehaviour Implementation
        #endregion
        #region Methods
        public TResult Convert(IItemWallet wallet)
        {
            var array = wallet.SearchOn(Model, null);
            return Convert(array);
        }

        public TResult Convert(IItem[] items)
        {
            if (items == null || (listenerIndex < 0 || listenerIndex >= items.Length)) {
                return default;
            } else {
                var item = items[listenerIndex] as TItem;
                return Convert(item);
            }
        }
        #endregion
    }
}