using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Currencies;

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