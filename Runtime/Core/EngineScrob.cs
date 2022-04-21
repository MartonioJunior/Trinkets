using UnityEngine;

namespace MartonioJunior.Collectables
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
    }
}