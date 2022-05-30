using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    public interface IItem: IResource
    {
        #region Properties
        IItemCategory Category {get;}
        string FilterName {get;}
        #endregion
        #region Methods
        void InstanceOn(IItemWallet destination);
        IItem[] GetInstancesOn(IItemWallet wallet);
        #endregion
    }
}