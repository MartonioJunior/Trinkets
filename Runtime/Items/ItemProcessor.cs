using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    public abstract class ItemProcessor<TItem, TResult>: EngineBehaviour, IResourceProcessor<IItemWallet, TResult> where TItem: class, IItem
    {
        #region Variables
        [SerializeField] Field<IItemModel> model = new Field<IItemModel>();
        [SerializeField, Min(0)] int listenerIndex = 0;
        #endregion
        #region Properties
        public IItemModel Model {
            get => model.Unwrap();
            set {
                model.Set(value);
            }
        }

        public int ListenerIndex {
            get => listenerIndex;
            set => listenerIndex = Mathf.Max(value, 0);
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
            return ProcessArray(array);
        }

        public TResult ProcessArray(IItem[] items)
        {
            if (items == null || (listenerIndex >= items.Length)) {
                return default;
            } else {
                var item = items[listenerIndex] as TItem;
                return Convert(item);
            }
        }
        #endregion
    }
}