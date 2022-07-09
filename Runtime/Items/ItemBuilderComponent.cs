using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Items
{
    [AddComponentMenu("Trinkets/Item/Item Giver")]
    public class ItemBuilderComponent: EngineBehaviour, IItemBuilder
    {
        #region Variables
        [SerializeField] Field<IItemBuilder> builder = new Field<IItemBuilder>();
        #endregion
        #region Properties
        public IItemBuilder Builder {
            get => builder.Unwrap();
            set => builder.Set(value);
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
        #region IItemBuilder Implementation
        public IItemModel Model => Builder?.Model;
        
        public void AddTo(IItemWallet wallet)
        {
            if (wallet == null) return;

            Builder.Model.AddTo(wallet);
            onCollectedItem?.Invoke();
        }
        #endregion
        #region Methods
        public void OnCollectedItem()
        {
            collectedItem?.Invoke();
        }
        #endregion
    }
}