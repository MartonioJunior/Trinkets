namespace MartonioJunior.Trinkets.Currency
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
}