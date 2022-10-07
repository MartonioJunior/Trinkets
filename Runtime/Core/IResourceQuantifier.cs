using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to determine the amount of resources contained
    in an object.</summary>
    */
    public interface IResourceQuantifier
    {
        /**
        <summary>Checks the amount of a resource inside.</summary>
        <param name="resource">The resource to check for.</param>
        <returns>The amount of resource available.</returns>
        */
        int AmountOf(IResource resource);
    }
}