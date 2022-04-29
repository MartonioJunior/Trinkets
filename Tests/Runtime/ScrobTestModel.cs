using UnityEngine;

namespace Tests
{
    public abstract class ScrobTestModel<T>: TestModel<T> where T: ScriptableObject
    {
        #region Abstract Implementation
        public abstract void ConfigureValues();
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            modelReference = ScriptableObject.CreateInstance<T>();
            ConfigureValues();
        }
        
        public override void DestroyTestContext()
        {
            ScriptableObject.DestroyImmediate(modelReference);
            modelReference = null;
        }
        #endregion
    }
}