using System.Collections.Generic;
using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using UnityEngine;
using NSubstitute;
using System.Collections;
using static Tests.Suite;

namespace Tests
{
    public partial class Mock
    {
        #region Classes
        public class MockResource: Resource
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
            #endregion
        }
        #endregion
        #region Test Cases
        public static IEnumerable ResourceDataCases()
        {
            yield return Array<ResourceData>(0, null);
            yield return Array<ResourceData>(Random.Range(1,10), Currencies);
            yield return Array<ResourceData>(Random.Range(1,10), Collectables);
            yield return Array<ResourceData>(Random.Range(1,10), MixCurrenciesAndCollectables);
        }
        #endregion
        #region Generators
        public static IResource IResource(bool quantifiable)
        {
            Substitute(out IResource resource);
            resource.Quantifiable.Returns(quantifiable);
            return resource;
        }

        public static ResourceData Collectables(int index = 0)
        {
            return new ResourceData(Substitute<ICollectable>());
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