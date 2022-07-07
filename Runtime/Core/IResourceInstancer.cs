using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceInstancer<T> where T: IWallet
    {
        void AddTo(T wallet);
    }
}