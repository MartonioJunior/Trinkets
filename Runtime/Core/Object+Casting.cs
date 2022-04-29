using UnityEngine;

namespace MartonioJunior.Collectables
{
    public static partial class ObjectExtensions
    {
        public static T Cast<T>(this object obj)
        {
            if (obj is T) {
                return (T) obj;
            } else {
                return default(T);
            }
        }

        public static T UnsafeCast<T>(this object obj)
        {
            return (T) obj;
        }
    }
}