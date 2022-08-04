using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow adding resources into an object.</summary>
    */
    public interface IResourceAdder<T> where T: IResource
    {
        /**
        <param name="resource">The resource to be added.</param>
        <returns><code>true</code> when the addition is successful.
        <code>false</code> when the addition fails.</returns>
        */
        bool Add(T resource);
    }
}