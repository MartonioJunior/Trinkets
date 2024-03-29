using System;
using System.Collections.Generic;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using MartonioJunior.Trinkets.Currencies;
using NSubstitute;
using Random = UnityEngine.Random;

namespace Tests
{
    public partial class Parameter
    {
        #region Static Methods
        public static T[] Array<T>(int size, Func<int, T> generator)
        {
            var result = new T[size];
            for(int i = 0; i < size; i++) {
                result[i] = generator.Invoke(i);
            }
            return result;
        }

        public static int Range(int minInclusive, int maxInclusive, int defaultValue, bool fixRandom = false)
        {
            if (fixRandom) return defaultValue;
            else return Random.Range(minInclusive, maxInclusive);
        }
        #endregion
    }
}