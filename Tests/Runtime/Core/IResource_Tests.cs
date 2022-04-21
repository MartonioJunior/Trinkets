using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using JurassicEngine;

namespace Tests.MartonioJunior.Collectables
{
    public class IResource_Tests: TestModel
    {
        #region Tests Required for IResource
        public interface Base {
            void Name_ReturnsNameOfResource();
            void Value_ReturnsResourceWorth();
        }
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext() {}
        public override void DestroyTestContext() {}
        #endregion
    }
}