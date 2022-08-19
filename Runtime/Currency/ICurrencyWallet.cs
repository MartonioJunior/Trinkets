namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Interface which describes a wallet for currencies.</summary>
    */
    public interface ICurrencyWallet: IResourceManager<ICurrency>, IWallet
    {
        #region Methods
        /**
        <summary>Adjusts the amount of a currency in the wallet.</summary>
        <param name="currency">The currency to be adjusted.</param>
        <param name="delta">The amount to be changed.<br/>
        Positive values increase the amount in the wallet.<br/>
        Negative values take away the amount from the wallet.</param>
        */
        void Change(ICurrency currency, int delta);
        /**
        <summary>Reset the amount of a currency back to zero.</summary>
        <param name="currency">The currency to be reset.</param>
        */
	    void Reset(ICurrency currency);
        #endregion
    }

    public static partial class ICurrencyWalletExtensions
    {
        /**
        <returns>The wallet where the resources were added in.</returns>
        <remarks>Useful for making multiple additions at once.</remarks>
        <inheritdoc cref="ICurrencyWallet.Change(ICurrency, int)"/>
        */
        public static ICurrencyWallet With(this ICurrencyWallet self, ICurrency currency, int delta)
        {
            if (currency != null) {
                self.Change(currency, delta);
            }

            return self;
        }
    }
}