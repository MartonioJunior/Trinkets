using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public abstract class EngineScrob: ScriptableObject
    {
        #region Abstract Methods
        public abstract void Reset();
        public abstract void Setup();
        public abstract void TearDown();
        public abstract void Validate();
        #endregion
        #region Methods
        private void Awake()
        {
            Validate();
            Reset();
            Setup();
        }

        private void OnValidate()
        {
            Validate();
        }

        private void OnDestroy()
        {
            TearDown();
        }
        #endregion
        #region Static Methods
        public static void Instance<T>(out T obj) where T: ScriptableObject
        {
            obj = ScriptableObject.CreateInstance<T>();
        }
        #endregion
    }
}