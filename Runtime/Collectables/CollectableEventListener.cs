using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Collectables
{
    [AddComponentMenu("Collectables/Collectable/Collectable Event Listener")]
    public class CollectableEventListener: EngineBehaviour, IResourceProcessor<ICollectableWallet, bool>
    {
        #region Constants
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        [SerializeField] Field<ICollectable> collectable = new Field<ICollectable>();
        [SerializeField] Field<ICollectableWallet> wallet = new Field<ICollectableWallet>();

        public ICollectable Collectable {
            get => collectable.Unwrap();
            set => collectable.Set(value);
        }

        public ICollectableWallet Wallet {
            get => wallet.Unwrap();
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
            if (wallet == null || !collectable.HasValue()) return false;

            return wallet.Contains(Collectable);
        }
        #endregion
        #region Methods
        private IEnumerator Start()
        {
            while (true) {
                var wasCollected = Convert(Wallet);
                onCollectableChange?.Invoke(wasCollected);

                yield return new WaitForSeconds(UpdateTime);
            }
        }

        private void OnCollectableChange(bool wasCollected)
        {
            collectableChanged?.Invoke(wasCollected);
        }
        #endregion
    }
}