using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Extension class for collections.</summary>
    */
    public static partial class CollectionExtensions
    {
        /**
        <summary>Changes the value on a dictionary by a certain amount</summary>
        <param name="self">The dictionary being used.</param>
        <param name="key">The key where the change should be performed.</param>
        <param name="delta">The amount to be changed.</param>
        <remarks>If the key is not on the dictionary, it is added in with the
        starting value of delta.</remarks>
        */
        public static void Delta<K>(this Dictionary<K, int> self, K key, int delta)
        {
            if (!self.ContainsKey(key)) self[key] = delta;
            else self[key] += delta;
        }
    }
}