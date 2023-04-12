using System.Collections.Generic;
using MartonioJunior.Trinkets;
using NUnit.Framework;

namespace Tests
{
    public static partial class Suite
    {
        #region Assert Methods
        public static void AssertResources<T>(IEnumerable<T> expected, IResourceQuantifier actual) where T: IResourceData
        {
            foreach (var item in expected) {
                Assert.AreEqual(item.Amount, actual.AmountOf(item.Resource));
            }
        }
        #endregion
    }
}