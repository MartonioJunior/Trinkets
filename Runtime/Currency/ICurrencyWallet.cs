namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Interface which describes a wallet for currencies.</summary>
    */
    public interface ICurrencyWallet: IWallet, ICurrencyOperator
    {
        
    }

    public static partial class ICurrencyWalletExtensions
    {
        /**
        <returns>The wallet where the resources were added in.</returns>
        <remarks>Useful for making multiple additions at once.</remarks>
        <inheritdoc cref="ICurrencyOperator.Change(ICurrency, int)"/>
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