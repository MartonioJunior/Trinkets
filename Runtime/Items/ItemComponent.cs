using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Items
{
    [AddComponentMenu("Collectables/Item")]
    public class ItemComponent: EngineBehaviour, IResourceInstancer<ItemWallet>
    {
        #region Variables
        [SerializeField] ItemData item;
        [SerializeField] int amount;
        #endregion
        #region Events
        [SerializeField] UnityEvent collectedItem;
        #endregion
        #region EngineBehaviour Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IResourceInstancer Implementation
        public void AddTo(ItemWallet wallet)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}