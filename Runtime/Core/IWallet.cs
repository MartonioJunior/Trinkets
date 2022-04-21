using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IWallet
    {
        IResource[] Contents();
    }
}