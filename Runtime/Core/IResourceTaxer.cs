using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceTaxer<T> where T: IWallet
    {
        void Tax(T wallet);
    }
}