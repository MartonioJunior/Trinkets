using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Items
{
    public abstract class ItemEventListener<T>: EngineBehaviour, IResourceProcessor<IItemWallet, T>
    {
        #region Constants
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        [SerializeField] Field<IItemWallet> wallet = new Field<IItemWallet>();
        [SerializeField] Field<IItem> item = new Field<IItem>();
        
        public IItemWallet Wallet {
            get => wallet.Unwrap();
            set => wallet.Set(value);
        }

        public IItem Item {
            get => item.Unwrap();
            set => item.Set(value);
        }
        #endregion
        #region Delegates
        public delegate void Event(T output);
        #endregion
        #region Events
        [SerializeField] UnityEvent<T> itemChanged;
        public event Event onItemChange;
        #endregion
        #region Abstract Methods
        public abstract T Convert(IItemWallet wallet);
        #endregion
        #region EngineBehaviour Implementation
        public override void Setup()
        {
            onItemChange += OnItemChange;
        }

        public override void TearDown()
        {
            onItemChange -= OnItemChange;
        }
        #endregion
        #region Methods
        private IEnumerator Start()
        {
            while (true) {
                var result = Convert(Wallet);
                onItemChange?.Invoke(result);

                yield return new WaitForSeconds(UpdateTime);
            }
        }

        private void OnItemChange(T output)
        {
            itemChanged?.Invoke(output);
        }
        #endregion
    }
}