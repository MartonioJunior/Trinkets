using UnityEngine;

namespace MartonioJunior.Collectables.Collectables
{
    public interface ICollectable: IResource
    {
        #region Properties
        ICollectableCategory Category { get; }
        #endregion
        #region Methods
        void Collect(ICollectableWallet destination);
        bool WasCollectedBy(ICollectableWallet wallet);
        #endregion
    }
}