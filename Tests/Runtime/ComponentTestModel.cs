using UnityEngine;

namespace Tests
{
    public abstract class ComponentTestModel<T>: TestModel<T> where T: MonoBehaviour
    {
        #region Constants
        protected const string ComponentEmptyInitialization = "Component is initialized without parameters";
        #endregion
        #region Variables
        protected GameObject gameObject;
        #endregion
        #region Abstract Methods
        public abstract void ConfigureValues();
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            gameObject = new GameObject($"{typeof(T)}-Test");
            modelReference = gameObject.AddComponent<T>();
            ConfigureValues();
        }
        
        public override void DestroyTestContext()
        {
            modelReference = null;
            GameObject.DestroyImmediate(gameObject);
            gameObject = null;
        }
        #endregion
    }
}