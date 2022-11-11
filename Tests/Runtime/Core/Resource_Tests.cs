using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets
{
    public class Resource_Tests: ScrobTestModel<Mock.MockResource>
    {
        #region Constants
        public const string DefaultName = "Default Name";
        private Sprite DefaultSprite;
        #endregion
        #region ScrobTestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.defaultName = DefaultName;
            modelReference.defaultImage = Value(Mock.Sprite, out DefaultSprite);
        }
        #endregion
        #region Method Tests
        [TestCase("Jewel", "Jewel")]
        [TestCase(null, DefaultName)]
        [TestCase("", DefaultName)]
        public void Name_ReturnsDesignationOfResource(string input, string output)
        {
            modelReference.Name = input;

            Assert.AreEqual(output, modelReference.Name);
        }

        [Test]
        public void Image_ReturnsIconOfResource()
        {
            void Verify(Sprite input, Sprite output)
            {
                modelReference.Image = input;

                Assert.AreEqual(output, modelReference.Image);
            }

            var AnySprite = Mock.Sprite;

            Verify(AnySprite, AnySprite);
            Verify(null, DefaultSprite);
        }

        [Test]
        public void Quantifiable_DefinesIfResourceIsCumulative([Values] bool value)
        {
            modelReference.quantifiable = value;

            Assert.AreEqual(value, modelReference.Quantifiable);
        }

        public static IEnumerable UseCase_Value()
        {
            var randomValue = Random.Range(1,1000);

            yield return new object[]{ randomValue, randomValue };
        }
        [TestCaseSource(nameof(UseCase_Value))]
        public void Value_ReturnsResourceWorth(int input, int output)
        {
            modelReference.value = input;

            Assert.AreEqual(output, modelReference.Value);
        }
        #endregion
    }
}