using System;
using NSubstitute;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests
{
    public static partial class Suite
    {
        #region Constants (Messages)
        public const string ComponentEmptyInitialization = "COMPONENT INITIALIZED WITHOUT PARAMETERS";
        public const string IncompleteImplementation = "INCOMPLETE TEST";
        public const string NotImplemented = "TEST NOT IMPLEMENTED";
        #endregion
        #region Static Methods
        public static T[] Array<T>(int size, Func<int, T> generator)
        {
            var result = new T[size];
            for (int i = 0; i < size; i++) {
                result[i] = generator.Invoke(i);
            }
            return result;
        }

        public static int Range(int minInclusive, int maxInclusive)
        {
            return Random.Range(minInclusive, maxInclusive);
        }

        public static int Range(int minInclusive, int maxInclusive, int defaultValue, bool fixedRandom = false)
        {
            if (fixedRandom) return defaultValue;
            else return Range(minInclusive, maxInclusive);
        }

        public static T Substitute<T>() where T: class
        {
            return NSubstitute.Substitute.For<T>();
        }

        public static T Substitute<T>(out T output) where T: class
        {
            output = Substitute<T>();
            return output;
        }

        public static T Value<T>(T value, out T output)
        {
            output = value;
            return value;
        }
        #endregion
    }
}