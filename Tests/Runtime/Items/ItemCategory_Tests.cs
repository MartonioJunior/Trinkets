using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Items;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemCategory_Tests: ScrobTestModel<ItemCategory>
    {
        #region Constants
        private const string CategoryName = "Armor";
        #endregion
        #region TestModel Implementation
        public override void CreateTestContext()
        {
            base.CreateTestContext();
        }

        public override void ConfigureValues()
        {
            modelReference.Name = CategoryName;
        }

        public override void DestroyTestContext()
        {
            base.DestroyTestContext();
        }
        #endregion
        #region Method Tests
        [Test]
        public void Name_ReturnsCategoryName()
        {
            Assert.AreEqual(CategoryName, modelReference.Name);
        }

        [Test]
        public void Name_ChangesToDefaultWhenSetToNullOrEmpty()
        {
            modelReference.Name = null;

            Assert.AreEqual(ItemCategory.DefaultDisplayName, modelReference.Name);
        }
        #endregion
    }
}