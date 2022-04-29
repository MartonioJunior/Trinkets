using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceProcessor<TInput, TOutput> where TInput: IWallet
    {
        TOutput Convert(TInput wallet);
    }
}