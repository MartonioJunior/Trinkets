using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to determine the amount of resources contained
    in an object.</summary>
    */
    public interface IResourceQuantifier<T> where T: IResource
    {
        /**
        <summary>Returns the amount of a resource inside.</summary>
        <param name="searchItem">The resource to check for.</param>
        <returns>The amount of resource available.</returns>
        */
        int AmountOf(T searchItem);
    }
}