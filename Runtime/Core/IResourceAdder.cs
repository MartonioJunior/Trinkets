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
        public static void AddRange(this IResourceAdder self, params IResourceData[] array)
        {
            foreach(var item in array) self.Add(item);   
        }

        public static void AddRange(this IResourceAdder self, ICollection<IResourceData> collection)
        {
            if (collection == null) return;

            foreach(var item in collection) self.Add(item);
        }

        public static void AddRange(this IResourceAdder self, ICollection<ResourceData> collection)
        {
            if (collection == null) return;

            foreach(var item in collection) self.Add(item);
        }
    }
}