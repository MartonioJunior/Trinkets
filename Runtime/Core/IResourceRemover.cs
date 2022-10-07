using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow removing resources from an object.</summary>
    */
    public interface IResourceRemover
    {
        /**
        <summary>Removes a resource from the object.</summary>
        <param name="data">What should be removed.</param>
        <returns><c>true</c> when the removal is successful.<br/>
        <c>false</c> when the removal fails.</returns>
        */
        bool Remove(IResourceData data);
    }
}