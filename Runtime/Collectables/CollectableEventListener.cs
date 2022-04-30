using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Collectables
{
    [AddComponentMenu("Collectables/Collectable/Collectable Event Listener")]
    public class CollectableEventListener: EngineBehaviour, IResourceProcessor<ICollectableWallet, bool>
    {
        #region Variables
        [SerializeField] Field<ICollectable> collectable;
        [SerializeField] Field<ICollectableWallet> wallet;

        public ICollectable Collectable {
            get => collectable.Unpack();
            set => collectable.Set(value);
        }

        public ICollectableWallet Wallet {
            get => wallet.Unpack();
            set => wallet.Set(value);
        }
        #endregion
        #region Delegates
        public delegate void Event(bool wasCollected);
        #endregion
        #region Events
        [SerializeField] UnityEvent<bool> collectableChanged;
        public event Event onCollectableChange;
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}

        public override void Setup()
        {
            onCollectableChange += OnCollectableChange;
            FixedUpdate();
        }

        public override void TearDown()
        {
            onCollectableChange -= OnCollectableChange;
        }

        public override void Validate() {}
        #endregion
        #region IResourceProcessor Implementation
        public bool Convert(ICollectableWallet wallet)
        {
            if (!collectable.HasValue()) return false;

            return wallet.Contains(Collectable);
        }
        #endregion
        #region Methods
        private void FixedUpdate()
        {
            var wasCollected = Convert(Wallet);
            onCollectableChange?.Invoke(wasCollected);
        }

        private void OnCollectableChange(bool wasCollected)
        {
            collectableChanged?.Invoke(wasCollected);
        }
        #endregion
    }
}