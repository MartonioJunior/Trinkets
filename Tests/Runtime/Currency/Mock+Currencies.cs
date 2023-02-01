using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using NSubstitute;

namespace Tests
{
    public static partial class Mock
    {
        #region Mock Types
        public static CurrencyWallet CurrencyWallet {
            get => ScriptableObject<CurrencyWallet>();
        }

        public static ICurrency ICurrency {
            get {
                var currency = Substitute.For<ICurrency>();
                currency.Quantifiable.Returns(true);
                return currency;
            }
        }

        public static CurrencyData Currency(string name)
        {
            ScriptableObject(out CurrencyData currency);
            currency.Name = name;
            currency.Image = Sprite();
            return currency;
        }
        #endregion
    }
}