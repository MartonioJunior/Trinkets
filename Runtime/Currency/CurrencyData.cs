using UnityEngine;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>ScriptableObject which defines a Currency.</summary>
    */
    [CreateAssetMenu(fileName = "NewCurrencyData", menuName = "Trinkets/Currency/Data")]
    public class CurrencyData: EngineScrob, ICurrency
    {
        #region Constants
        /**
        <summary>Default Name used when the name of a <c>CurrencyData</c>
        is empty or null.</summary>
        */
        public const string DefaultDisplayName = "Unnamed Currency";
        #endregion
        #region Variables
        /**
        <inheritdoc cref="IRepresentable.Image"/>
        */
        [SerializeField] Sprite displayIcon;
        /**
        <inheritdoc cref="IRepresentable.Name"/>
        */
        [SerializeField] string displayName;
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
        /**
        <inheritdoc />
        */
        public override void Validate()
        {
            if (string.IsNullOrEmpty(displayName)) {
                displayName = DefaultDisplayName;
            }
        }
        #endregion
        #region ICurrency Implementation
        /**
        <inheritdoc />
        */
        public string Name {
            get => displayName;
            set {
                displayName = value;
                Validate();
            }
        }
        /**
        <inheritdoc />
        */
        public Sprite Image {
            get => displayIcon;
            set => displayIcon = value;
        }
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
        public int Value {
            get => currencyRateValue;
            set => currencyRateValue = Mathf.Max(0, value);
        }
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
            return $"{displayName} ({symbol})";
        }
        #endregion
    }
}