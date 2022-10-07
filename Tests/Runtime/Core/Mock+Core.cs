using System.Collections.Generic;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using UnityEngine;
using NSubstitute;
using MartonioJunior.Trinkets.Currencies;
using System.Collections;

namespace Tests
{
    public partial class Mock
    {
        #region Test Cases
        public static IEnumerable ResourceDataCases()
        {
            yield return Parameter.Array<ResourceData>(0, null);
            yield return Parameter.Array<ResourceData>(Random.Range(1,10), Currencies);
            yield return Parameter.Array<ResourceData>(Random.Range(1,10), Collectables);
            yield return Parameter.Array<ResourceData>(Random.Range(1,10), MixCurrenciesAndCollectables);
        }
        #endregion
        #region Generators
        public static ResourceData Collectables(int index = 0)
        {
            return new ResourceData(Substitute.For<ICollectable>());
        }

        public static ResourceData Currencies(int index = 0)
        {
            return new ResourceData(Substitute.For<ICurrency>(), Random.Range(1,10000));
        }

        private static ResourceData MixCurrenciesAndCollectables(int index)
        {
            return (index % 2 == 0) ? Collectables(index) : Currencies(index);
        }
        #endregion
    }
}