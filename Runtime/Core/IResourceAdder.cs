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

    public static partial class IResourceAdderExtensions
    {
        /**
        <summary>Adds a range of resources.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="array">Collection of resources to be added.</param>
        */
        public static void AddRange(this IResourceAdder self, params IResourceData[] array)
        {
            foreach(var item in array) self.Add(item);   
        }
        /**
        <param name="collection">Collection of resources to be added.</param>
        <inheritdoc cref="IResourceAdderExtensions.AddRange(IResourceAdder, IResourceData[])"/>
        */
        public static void AddRange(this IResourceAdder self, ICollection<IResourceData> collection)
        {
            if (collection == null) return;

            foreach(var item in collection) self.Add(item);
        }
        /**
        <inheritdoc cref="IResourceAdderExtensions.AddRange(IResourceAdder, ICollection{IResourceData})"/>
        */
        public static void AddRange(this IResourceAdder self, ICollection<ResourceData> collection)
        {
            if (collection == null) return;

            foreach(var item in collection) self.Add(item);
        }
    }
}