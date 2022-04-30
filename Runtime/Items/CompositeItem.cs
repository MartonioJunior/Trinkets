using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    public abstract class CompositeItem: ItemData
    {
        #region ICompositeItem Implementation
        public abstract void Add(IItem element);
        public abstract IItem[] GetChildren();
        public abstract void Remove(IItem element);
        #endregion
    }
}