using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Collectables.Currency;

namespace Tests.MartonioJunior.Collectables.Currency
{
    public class CurrencyData_Tests: ScrobTestModel<CurrencyData>
    {
        #region Constants
        public const string DisplayName = "Real";
        public const string Symbol = "R$";
        public const int Value = 3;
        #endregion
        #region ScrobTestModel Implementation
        public override void ConfigureValues()
        {
            modelReference.Name = DisplayName;
            modelReference.Value = Value;
            modelReference.Symbol = Symbol;
        }
        #endregion
        #region Test Methods
        [Test]
        public void Name_ReturnsNameOfResource()
        {
            Assert.AreEqual(DisplayName, modelReference.Name);
        }

        [Test]
        public void Value_ReturnsResourceWorth()
        {
            Assert.AreEqual(Value, modelReference.Value);
        }

        [Test]
        public void Value_CannotBeNegative()
        {
            modelReference.Value = -Value;

            Assert.False(modelReference.Value < 0);
        }

        [Test]
        public void Symbol_ReturnsCurrencyIndicator()
        {
            Assert.AreEqual(Symbol, modelReference.Symbol);
        }

        [Test]
        public void Validate_UsesDefaultDisplayNameOnEmptyString()
        {
            modelReference.Name = null;
            modelReference.Validate();

            Assert.AreEqual(CurrencyData.DefaultDisplayName, modelReference.Name);
        }
        #endregion
    }
}