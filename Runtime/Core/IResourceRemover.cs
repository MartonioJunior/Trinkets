using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow removing resources from an object.</summary>
    */
    public interface IResourceRemover<T> where T: IResource
    {
        /**
        <summary>Removes a resource from the object.</summary>
        <returns><code>true</code> when the resource is removed successfully.
        <code>false</code> when the removal fails.</returns>
        */
        bool Remove(T resource);
    }
}