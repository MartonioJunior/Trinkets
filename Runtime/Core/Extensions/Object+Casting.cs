using UnityEngine;

namespace MartonioJunior.Collectables
{
    public static partial class ObjectExtensions
    {
        public static T UnsafeCast<T>(this object obj)
        {
            return (T) obj;
        }
    }
}