using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Items;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemModel_Tests: ScrobTestModel<ItemModel_Dummy>
    {
        #region Variables
        private const string ItemName = "Fire Rod";
        private const int ItemValue = 24;
        private Sprite Icon;
        #endregion
        #region TestModel Implementation
        public override void ConfigureValues()
        {
            Icon = Sprite.Create(Texture2D.grayTexture, new Rect(), Vector2.zero);

            modelReference.Image = Icon;
            modelReference.Name = ItemName;
            modelReference.Value = ItemValue;
        }

        public override void DestroyTestContext()
        {
            Sprite.DestroyImmediate(Icon);

            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Image_ReturnsModelIcon()
        {
            Assert.AreEqual(Icon, modelReference.Image);
        }

        [Test]
        public void Model_ReturnsItself()
        {
            Assert.AreEqual(modelReference, modelReference.Model);
        }

        [Test]
        public void Name_ReturnsItemName()
        {
            Assert.AreEqual(ItemName, modelReference.Name);
        }

        [Test]
        public void ToString_ReturnsModelName()
        {
            Assert.AreEqual(ItemName, modelReference.ToString());
        }

        [Test]
        public void Value_ReturnsItemWorth()
        {
            Assert.AreEqual(ItemValue, modelReference.Value);
        }
        #endregion
    }
}