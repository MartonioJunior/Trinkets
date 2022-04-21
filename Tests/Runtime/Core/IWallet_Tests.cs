using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using JurassicEngine;
using JurassicEngine.Mechanics.Resources;

namespace Tests.MartonioJunior.Collectables
{
    public class IWallet_Tests: TestModel<IWallet>
    {
        #region Tests Required for IResource
        public interface Base
        {
            void Contents_ReturnsListOfStoredResources();
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