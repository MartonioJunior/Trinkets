using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceInstancer<T> where T: IWallet
    {
        void AddTo(T wallet);
    }
}