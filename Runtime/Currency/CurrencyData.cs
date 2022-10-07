using UnityEngine;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>ScriptableObject which defines a Currency.</summary>
    */
    [CreateAssetMenu(fileName = "New Currency", menuName = "Trinkets/Currency/New Currency")]
    public class CurrencyData: Resource, ICurrency
    {
        #region Constants
        /**
        <summary>Default Name used when the name of a <c>CurrencyData</c>
        is empty or null.</summary>
        */
        public const string DefaultCurrencyName = "Unnamed Currency";
        #endregion
        #region Variables
        /**
        <inheritdoc cref="ICurrency.Symbol"/>
        */
        [SerializeField] string symbol;
        /**
        <summary>The value that a single unit of currency has.</summary>
        <remarks>Useful to make conversions between currencies.</remarks>
        */
        [SerializeField, Min(0)] int currencyRateValue = 1;
        #endregion
        #region EngineScrob Implementation
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        #endregion
        #region ICurrency Implementation
        /**
        <inheritdoc />
        */
        public override string DefaultName => DefaultCurrencyName;
        /**
        <inheritdoc />
        */
        public override Sprite DefaultImage => null;
        /**
        <inheritdoc />
        */
        public string Symbol {
            get => symbol;
            set => symbol = value;
        }
        /**
        <inheritdoc />
        */
        public override int Value {
            get => currencyRateValue;
            set => currencyRateValue = Mathf.Max(0, value);
        }
        /**
        <inheritdoc />
        */
        public override bool Quantifiable => true;
        #endregion
        #region Methods
        /**
        <summary>Returns a visual description of the currency</summary>
        <returns>The currency's details."</returns>
        <example>A <c>CurrencyData</c> named "Coin" with the Symbol "g" returns
        "Coin (g)"</example>
        */
        public override string ToString()
        {
            return $"{Name} ({symbol})";
        }
        #endregion
        #region Operators
        public static ResourceData operator *(CurrencyData lhs, int rhs)
        {
            return new ResourceData(lhs, rhs);
        }
        #endregion
    }
}