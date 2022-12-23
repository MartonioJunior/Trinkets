using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to describe a wallet of resources.</summary>
    */
    public interface IWallet: IResourceGroup
    {
        /**
        <summary>Describes the contents inside a wallet.</summary>
        */
        IResourceGroup Contents {get;}
    }
}