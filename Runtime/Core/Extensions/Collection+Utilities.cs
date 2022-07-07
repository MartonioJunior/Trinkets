using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public static partial class CollectionExtensions
    {
        #region Dictionary
        public static void Delta<K>(this Dictionary<K, int> self, K key, int delta)
        {
            if (!self.ContainsKey(key)) self[key] = delta;
            else self[key] += delta;
        }
        #endregion
        #region List
        public static int CountMatches<T>(this List<T> self, Predicate<T> predicate)
        {
            int count = 0;
            if(predicate == null) return count;

			for(var iterator = self.GetEnumerator(); iterator.MoveNext();) {
				if(predicate(iterator.Current)) count++;
			}

			return count;
        }

        public static bool HasAll<T>(this List<T> self, Predicate<T> predicate)
        {
            if(predicate == null) return false;

			for(var iterator = self.GetEnumerator(); iterator.MoveNext();) {
				if(!predicate(iterator.Current)) return false;
			}

			return true;
        }
        #endregion
    }
}