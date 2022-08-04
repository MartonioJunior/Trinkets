using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
    [AddComponentMenu("Trinkets/Collectable/Collectable Giver")]
    public class CollectableComponent: EngineBehaviour, IResourceInstancer<ICollectableWallet>
    {
        #region Variables
        [SerializeField] Field<ICollectable> collectable = new Field<ICollectable>();

        public ICollectable Collectable {
            get => collectable.Unwrap();
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
        public override void Setup()
        {
            onCollected += OnCollected;
        }

        public override void TearDown()
        {
            onCollected -= OnCollected;
        }
        #endregion
        #region IResourceInstancer Implementation
        public void AddTo(ICollectableWallet wallet)
        {
            if (enabled && collectable.HasValue()) {
                bool newAddition = wallet.Add(Collectable);
                onCollected?.Invoke(newAddition);
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
            collectedEvent?.Invoke(newlyAdded);
        }
        #endregion
    }
}