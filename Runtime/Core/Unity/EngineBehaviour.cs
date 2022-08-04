using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public abstract class EngineBehaviour: MonoBehaviour
    {
        #region Abstract Methods
        public abstract void Setup();
        public abstract void TearDown();
        #endregion
        #region Methods
        private void Awake()
        {
            Setup();
        }

        private void OnDestroy()
        {
            TearDown();
        }
        #endregion
    }
}