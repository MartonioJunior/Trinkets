using UnityEngine;

namespace MartonioJunior.Collectables.Currency
{
    [CreateAssetMenu(fileName="NewCurrencyData", menuName="Collectables/Currency/Data")]
    public class CurrencyData: EngineScrob, ICurrency
    {
        #region Constants
        public const string DefaultDisplayName = "Unnamed Currency";
        #endregion
        #region Variables
        [SerializeField] Sprite displayIcon;
        [SerializeField] string displayName;
        [SerializeField] string symbol;
        [SerializeField, Min(0)] int currencyRateValue;
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
    }
}