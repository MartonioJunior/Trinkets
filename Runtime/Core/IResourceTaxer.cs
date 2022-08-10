using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used for removing resources from a wallet.</summary>
    */
    public interface IResourceTaxer<T> where T: IWallet
    {
        /**
        <summary>Removes resources from a wallet.</summary>
        <param name="wallet">The wallet that'll have resources taken away.</param>
        */
        void Tax(T wallet);
    }
}