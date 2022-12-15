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

    public static partial class IResourceRemoverExtensions
    {
        /**
        <summary>Removes a range of resources.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="array">Collection of items to remove.</param>
        */
        public static void RemoveRange(this IResourceRemover self, params IResourceData[] array)
        {
            foreach(var item in array) self.Remove(item);   
        }
        /**
        <param name="collection">Collection of items to remove</param>
        <inheritdoc cref="IResourceRemoverExtensions.RemoveRange(IResourceRemover, IResourceData[])"/>
        */
        public static void RemoveRange(this IResourceRemover self, ICollection<IResourceData> collection)
        {
            foreach(var item in collection) self.Remove(item);
        }
        /**
        <inheritdoc cref="IResourceRemoverExtensions.RemoveRange(IResourceRemover, ICollection{IResourceData})"/>
        */
        public static void RemoveRange(this IResourceRemover self, ICollection<ResourceData> collection)
        {
            foreach(var item in collection) self.Remove(item);
        }
    }
}