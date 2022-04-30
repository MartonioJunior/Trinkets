using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Collectables
{
    [AddComponentMenu("Collectables/Collectable/Collectable Giver")]
    public class CollectableComponent: EngineBehaviour, IResourceInstancer<ICollectableWallet>
    {
        #region Variables
        [SerializeField] Field<ICollectable> collectable;

        public ICollectable Collectable {
            get => collectable.Unpack();
            set => collectable.Set(value);
        }
        #endregion
        #region Delegate
        public delegate void Event(bool newlyAdded);
        #endregion
        #region Events
        [SerializeField] UnityEvent<bool> collectedEvent;
        public event Event onCollected;
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}
        public override void Setup()
        {
            onCollected += OnCollected;
        }

        public override void TearDown()
        {
            onCollected -= OnCollected;
        }

        public override void Validate() {}
        #endregion
        #region IResourceInstancer Implementation
        public void AddTo(ICollectableWallet wallet)
        {
            if (enabled && collectable.HasValue()) {
                bool newAddition = wallet.Add(Collectable);
                OnCollected(newAddition);
            }
        }
        #endregion
        #region Methods
        public void AddToWallet(CollectableWallet wallet)
        {
            AddTo(wallet);
        }

        private void OnCollected(bool newlyAdded)
        {
            collectedEvent.Invoke(newlyAdded);
        }
        #endregion
    }
}