using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using JurassicEngine;
using JurassicEngine.Mechanics.Resources;

namespace Tests.MartonioJunior.Collectables
{
    public class IResourceCategory_Tests: TestModel<IResourceCategory>
    {
        #region Tests Required for IResource
        public interface Base
        {
            void Name_ReturnsNameOfResource();
        }
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext() {}
        public override void DestroyTestContext() {}
        #endregion
        #region Method Tests
        #endregion
    }
}