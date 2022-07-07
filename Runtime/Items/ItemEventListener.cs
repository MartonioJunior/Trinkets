using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Items
{
    public abstract class ItemEventListener<T>: EngineBehaviour
    {
        #region Constants
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        [SerializeField] Field<IItemWallet> wallet = new Field<IItemWallet>();
        [SerializeField] Field<IItemModel> model = new Field<IItemModel>();
        
        public IItemWallet Wallet {
            get => wallet.Unwrap();
            set => wallet.Set(value);
        }

        public IItemModel Model {
            get => model.Unwrap();
            set => model.Set(value);
        }
        #endregion
        #region Delegates
        public delegate void Event(T[] output);
        #endregion
        #region Events
        [SerializeField] UnityEvent<T[]> collectionChanged;
        public event Event onCollectionChange;
        #endregion
        #region Abstract Methods
        public abstract T[] Convert(IItem[] items);
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}
        public override void Setup()
        {
            onCollectionChange += OnCollectionChange;
        }

        public override void TearDown()
        {
            onCollectionChange -= OnCollectionChange;
        }
        public override void Validate() {}
        #endregion
        #region Methods
        private IEnumerator Start()
        {
            while (true) {
                var itemArray = Wallet.SearchOn(Model, null);
                var result = Convert(itemArray);
                onCollectionChange?.Invoke(result);

                yield return new WaitForSeconds(UpdateTime);
            }
        }

        private void OnCollectionChange(T[] output)
        {
            collectionChanged?.Invoke(output);
        }
        #endregion
    }
}