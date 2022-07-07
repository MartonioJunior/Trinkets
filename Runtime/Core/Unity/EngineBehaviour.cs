using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public abstract class EngineBehaviour: MonoBehaviour
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
            OnValidate();
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