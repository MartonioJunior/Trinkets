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
        <summary>The value that a single unit of currency has.</summary>
        <remarks>Useful to make conversions between currencies.</remarks>
        */
        [SerializeField, Min(0)] int currencyRateValue = 1;
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
        [field: SerializeField] public string Symbol {get; set;}
        /**
        <inheritdoc />
        */
        public override int Value {
            get => currencyRateValue;
            set => currencyRateValue = Mathf.Max(0, value);
        }
        /**
        <remarks>Currencies always return <c>true</c>.</remarks>
        <inheritdoc />
        */
        public override bool Quantifiable => true;
        #endregion
        #region Methods
        /**
        <summary>Returns a visual description of the currency</summary>
        <returns>A string containing the name and symbol of the currency.</returns>
        <example>A <c>CurrencyData</c> named "Coin" with the Symbol "g" returns
        "Coin (g)"</example>
        */
        public override string ToString()
        {
            return $"{Name} ({Symbol})";
        }
        #endregion
        #region Operators
        /**
        <summary>Creates a <c>ResourceData</c> by multiplying a currency with
        an integer.</summary>
        */
        public static ResourceData operator *(CurrencyData lhs, int rhs)
        {
            return new ResourceData(lhs, rhs);
        }
        #endregion
    }
}