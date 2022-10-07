using UnityEngine;

namespace Tests
{
    public abstract class ComponentTestModel<T>: TestModel<T> where T: MonoBehaviour
    {
        #region Constants
        protected const string ComponentEmptyInitialization = "Component is initialized without parameters";
        #endregion
        #region Abstract
        public abstract void ConfigureValues();
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = Mock.GameObject($"{typeof(T)}").AddComponent<T>();
            ConfigureValues();
        }
        
        public override void DestroyTestContext()
        {
            modelReference = null;
        }
        #endregion
    }
}