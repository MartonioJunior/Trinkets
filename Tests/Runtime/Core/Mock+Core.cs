using System.Collections.Generic;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using UnityEngine;
using NSubstitute;
using System.Collections;

namespace Tests
{
    public partial class Mock
    {
        #region Classes
        public class MockResource : Resource
        {
            #region Variables
            public string defaultName;
            public Sprite defaultImage;
            public bool quantifiable;
            public int value;
            #endregion
            #region Resource Implementation
            public override string DefaultName => defaultName;
            public override Sprite DefaultImage => defaultImage;
            public override int Value { get => value; set => this.value = value; }
            public override bool Quantifiable => quantifiable;
            public override void Setup() {}
            public override void TearDown() {}
            #endregion
        }
        #endregion
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
            return new ResourceData(Mock.ICurrency, Random.Range(1,10000));
        }

        public static ResourceData MixCurrenciesAndCollectables(int index)
        {
            return (index % 2 == 0) ? Collectables(index) : Currencies(index);
        }
        #endregion
    }
}