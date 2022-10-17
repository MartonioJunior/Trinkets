using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;
using NSubstitute;

namespace Tests
{
    public partial class Mock
    {
        #region Mock Types
        public CurrencyWallet CurrencyWallet {
            get {
                EngineScrob.Instance(out CurrencyWallet wallet);
                objectList.Add(wallet);
                return wallet;
            }
        }

        public static ICurrency ICurrency {
            get {
                var currency = Substitute.For<ICurrency>();
                currency.Quantifiable.Returns(true);
                return currency;
            }
        }

        public CurrencyData Currency(string name)
        {
            EngineScrob.Instance(out CurrencyData currency);
            currency.Name = name;
            currency.Image = this.Sprite;
            objectList.Add(currency);
            return currency;
        }
        #endregion
    }
}