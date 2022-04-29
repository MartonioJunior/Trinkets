using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceTaxer<T> where T: IWallet
    {
        void Tax(T wallet);
    }
}