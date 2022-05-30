using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Items
{
    [AddComponentMenu("Collectables/Items/Item Drop")]
    public class ItemDropComponent: EngineBehaviour, IResourceInstancer<IItemWallet>
    {
        #region Variables
        [SerializeField] Field<IItem> item = new Field<IItem>();

        public IItem Item {
            get => item.Unwrap();
            set => item.Set(value);
        }
        #endregion
        #region Delegates
        public delegate void Event();
        #endregion
        #region Events
        [SerializeField] UnityEvent collectedItem;
        public event Event onCollectedItem;
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}

        public override void Setup()
        {
            onCollectedItem += OnCollectedItem;
        }

        public override void TearDown()
        {
            onCollectedItem -= OnCollectedItem;
        }

        public override void Validate() {}
        #endregion
        #region IResourceInstancer Implementation
        public void AddTo(IItemWallet wallet)
        {
            if (wallet == null) return;

            if (item.HasValue()) {
                Item.InstanceOn(wallet);
                onCollectedItem?.Invoke();
            }
        }
        #endregion
        #region Methods
        private void OnCollectedItem()
        {
            collectedItem?.Invoke();
        }
        #endregion
    }
}