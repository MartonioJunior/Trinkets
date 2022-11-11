using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to obtain data from a resource group.</summary>
    */
    public interface IResourceProcessor<T>
    {
        /**
        <summary>Transform group information into data.</summary>
        <param name="group">The group used as the source of data.</param>
        <returns>The data acquired from the operation.</returns>
        */
        T Convert(IResourceGroup group);
    }
}