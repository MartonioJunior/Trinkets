using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    public interface IItem: IResource, IItemBuilder
    {
        #region Methods
        IItem Copy();
        #endregion
    }
}