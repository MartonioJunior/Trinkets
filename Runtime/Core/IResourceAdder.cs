using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow adding resources into an object.</summary>
    */
    public interface IResourceAdder
    {
        /**
        <summary>Adds a resource to the object.</summary>
        <param name="data">The data to be added.</param>
        <returns><c>true</c> when the addition is successful.<br/>
        <c>false</c> when the addition fails.</returns>
        */
        bool Add(IResourceData data);
    }
    /**
    <summary>Extension class for <c>IResourceAdder</c></summary>
    */
    public static partial class IResourceAdderExtensions
    {
        /**
        <summary>Adds a range of resources.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="array">Collection of resources to be added.</param>
        */
        public static void Add(this IResourceAdder self, params IResourceData[] array)
        {
            self.AddRange(array);
        }
        /**
        <param name="itemsToAdd">Collection of resources to be added.</param>
        <inheritdoc cref="IResourceAdderExtensions.Add(IResourceAdder, IResourceData[])"/>
        */
        public static void AddRange<T>(this IResourceAdder self, IEnumerable<T> itemsToAdd) where T: IResourceData
        {
            if (itemsToAdd == null) return;

            foreach (var item in itemsToAdd) self.Add(item);
        }
    }
}