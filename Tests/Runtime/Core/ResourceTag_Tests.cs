using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets;

namespace Tests.MartonioJunior.Trinkets
{
    public class ResourceTag_Tests: ScrobTestModel<ResourceTag>
    {
        #region TestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Method Tests
        [TestCase("Simple")]
        [TestCase("")]
        [TestCase(null)]
        public void Name_ReturnsTagName(string name)
        {
            modelReference.Name = name;

            Assert.AreEqual(name, modelReference.Name);
        }

        [Test]
        public void Image_ReturnsIconOfTag()
        {
            void Verify(Sprite input, Sprite output) {
                modelReference.Image = Value(Mock.Sprite, out Sprite sprite);

                Assert.AreEqual(sprite, modelReference.Image);
            }

            var AnySprite = Mock.Sprite;

            Verify(AnySprite, AnySprite);
            Verify(null, null);
        }
        #endregion
    }
}