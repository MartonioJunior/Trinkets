using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceProcessor<TInput, TOutput> where TInput: IWallet
    {
        TOutput Convert(TInput wallet);
    }
}