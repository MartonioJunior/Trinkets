using UnityEngine;

namespace MartonioJunior.Trinkets.Currency
{
    [CreateAssetMenu(fileName = "NewCurrencyData", menuName = "Trinkets/Currency/Data")]
    public class CurrencyData: EngineScrob, ICurrency
    {
        #region Constants
        public const string DefaultDisplayName = "Unnamed Currency";
        #endregion
        #region Variables
        [SerializeField] Sprite displayIcon;
        [SerializeField] string displayName;
        [SerializeField] string symbol;
        [SerializeField, Min(0)] int currencyRateValue = 1;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate()
        {
            if (string.IsNullOrEmpty(displayName)) {
                displayName = DefaultDisplayName;
            }
        }
        #endregion
        #region ICurrency Implementation
        public string Name {
            get => displayName;
            set {
                displayName = value;
                Validate();
            }
        }

        public Sprite Image {
            get => displayIcon;
            set => displayIcon = value;
        }

        public string Symbol {
            get => symbol;
            set => symbol = value;
        }

        public int Value {
            get => currencyRateValue;
            set => currencyRateValue = Mathf.Max(0, value);
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"{displayName} ({symbol})";
        }
        #endregion
    }
}