using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.Trinkets.Currencies;
using static Tests.Suite;

namespace Tests.MartonioJunior.Trinkets.Currencies
{
    public class CurrencyData_Tests: ScrobTestModel<CurrencyData>
    {
        #region ScrobTestModel Implementation
        public override void ConfigureValues() {}
        #endregion
        #region Test Methods
        [Test]
        public void Image_ReturnsIconOfResource()
        {
            modelReference.Image = Value(Mock.Sprite(), out var Icon);

            Assert.AreEqual(Icon, modelReference.Image);
        }

        [TestCase("Lollipop", "Lollipop")]
        [TestCase("", CurrencyData.DefaultCurrencyName)]
        [TestCase(null, CurrencyData.DefaultCurrencyName)]
        public void Name_ReturnsNameOfResource(string input, string output)
        {
            modelReference.Name = input;

            Assert.AreEqual(output, modelReference.Name);
        }

        public static IEnumerable UseCases_Value()
        {
            var positiveAmount = Range(0,1000);
            var negativeAmount = Range(-1000,-1);

            yield return new object[]{positiveAmount, positiveAmount};
            yield return new object[]{negativeAmount, 0};
        }
        [TestCaseSource(nameof(UseCases_Value))]
        public void Value_ReturnsResourceWorth(int input, int output)
        {
            modelReference.Value = input;

            Assert.AreEqual(output, modelReference.Value);
        }

        [TestCase("R$")]
        [TestCase("z")]
        [TestCase("🟩")]
        public void Symbol_ReturnsCurrencyIndicator(string symbol)
        {
            modelReference.Symbol = symbol;

            Assert.AreEqual(symbol, modelReference.Symbol);
        }

        [TestCase("Real", "R$", "Real (R$)")]
        [TestCase("Gold", "g", "Gold (g)")]
        [TestCase("Emerald", "🟩", "Emerald (🟩)")]
        public void ToString_ReturnsCurrencyNameAndSymbol(string name, string symbol, string output)
        {
            modelReference.Name = name;
            modelReference.Symbol = symbol;

            Assert.AreEqual(output, modelReference.ToString());
        }

        public static IEnumerable UseCases_Multiply()
        {
            var positiveAmount = Range(0,1000);
            var negativeAmount = Range(-1000,-1);

            yield return new object[]{positiveAmount, positiveAmount};
            yield return new object[]{negativeAmount, 0};
        }
        [TestCaseSource(nameof(UseCases_Multiply))]
        public void Multiply_CurrencyData_int_CreatesResourceData(int input, int output)
        {
            var result = modelReference * input;

            Assert.AreEqual(modelReference, result.Resource);
            Assert.AreEqual(output, result.Amount);
        }
        #endregion
    }
}