using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using JurassicEngine;
using JurassicEngine.Mechanics.Resources;

namespace Tests.MartonioJunior.Collectables
{
    public class IResourceTaxer_Tests: TestModel<IResourceTaxer<IWallet>>
    {
        #region Classes
        public interface Base
        {
            void CanBeTaxed_ReturnsTrueIfWalletHasTheResources();
            void Tax_RemovesResourceFromTheWallet();
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