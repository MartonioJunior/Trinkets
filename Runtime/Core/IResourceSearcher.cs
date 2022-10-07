using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to search for resources inside an object.</summary>
    */
    public interface IResourceSearcher
    {
        /**
        <summary>Searches for resources inside a data source.</summary>
        <param name="predicate">The filter used by the search operation.</param>
        <returns>An array of resources.</returns>
        */
        ICollection<IResourceData> Search(Predicate<IResourceData> predicate);
    }
}