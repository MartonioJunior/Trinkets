using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Abstract ScriptableObject which requires implementation for
    certain aspects of the lifecycle.</summary>
    */
    public abstract class EngineScrob: ScriptableObject
    {
        #region Abstract
        /**
        <summary>Configures the object after being awoken.</summary>
        <remarks>Ideal for linking events and defining base values.</remarks>
        */
        public abstract void Setup();
        /**
        <summary>Called when the object goes out of scope.</summary>
        <remarks>Ideal for disposing of objects and unlinking events.</remarks>
        */
        public abstract void TearDown();
        #endregion
        #region Methods
        /**
        <inheritdoc />
        */
        private void Awake()
        {
            Validate();
            Setup();
        }
        /**
        <inheritdoc />
        */
        private void OnValidate()
        {
            Validate();
        }
        /**
        <inheritdoc />
        */
        private void OnDestroy()
        {
            TearDown();
        }
        /**
        <summary>Called when the object goes out of scope.</summary>
        <remarks>Ideal for validating data before the <c>Setup</c> method
        is called.</remarks>
        */
        public virtual void Validate() {}
        #endregion
        #region Static Methods
        /**
        <summary>ScriptableObject instancing method which automatically detects the
        variable type.</summary>
        <param name="obj">The newly instanced ScriptableObject.</obj>
        */
        public static void Instance<T>(out T obj) where T: ScriptableObject
        {
            obj = ScriptableObject.CreateInstance<T>();
        }
        #endregion
    }
}