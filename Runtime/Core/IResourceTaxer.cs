using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used for removing resources from a group.</summary>
    */
    public interface IResourceTaxer
    {
        /**
        <summary>Removes resources from a group.</summary>
        <param name="group">The group that'll have resources taken away.</param>
        */
        void Tax(IResourceGroup group);
    }
}