using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceTaxer<T> where T: IWallet
    {
        bool CanBeTaxed(T wallet);
        void Tax(T wallet);
    }
}