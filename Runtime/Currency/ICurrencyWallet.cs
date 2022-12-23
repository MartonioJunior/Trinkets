namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Interface which describes a wallet for currencies.</summary>
    */
    public interface ICurrencyWallet: IWallet, ICurrencyOperator
    {
        
    }
    /**
    <summary>Extension class for <c>ICurrencyWallet</c></summary>
    */
    public static partial class ICurrencyWalletExtensions
    {
        /**
        <returns>The wallet where the resources were added in.</returns>
        <remarks>Useful for making multiple additions at once.</remarks>
        <param name="self">The extension object used by the operation.</param>
        <inheritdoc cref="ICurrencyOperator.Change(ICurrency, int)" />
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