using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Abstract ScriptableObject which requires implementation for
    certain aspects of the lifecycle.</summary>
    */
    public static class Engine
    {
        #region Static Methods
        /**
        <summary>ScriptableObject instancing method which automatically detects the
        variable type.</summary>
        <param name="obj">Newly instanced ScriptableObject.</param>
        */
        public static void Instance<T>(out T obj) where T: ScriptableObject
        {
            obj = ScriptableObject.CreateInstance<T>();
        }
        #endregion
    }
}