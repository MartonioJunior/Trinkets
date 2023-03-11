using System.Collections.Generic;
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
    /**
    <summary>Extension class for <c>IResourceRemover</c></summary>
    */
    public static partial class IResourceRemoverExtensions
    {
        /**
        <summary>Removes a range of resources.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="array">Collection of items to remove.</param>
        */
        public static void Remove(this IResourceRemover self, params IResourceData[] array)
        {
            self.RemoveRange(array);   
        }
        /**
        <param name="itemsToRemove">Collection of items to remove</param>
        <inheritdoc cref="IResourceRemoverExtensions.Remove(IResourceRemover, IResourceData[])"/>
        */
        public static void RemoveRange<T>(this IResourceRemover self, IEnumerable<T> itemsToRemove) where T: IResourceData
        {
            if (itemsToRemove == null) return;

            foreach (var item in itemsToRemove) self.Remove(item);
        }
    }
}