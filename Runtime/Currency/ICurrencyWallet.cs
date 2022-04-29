namespace MartonioJunior.Collectables.Currency
{
    public interface ICurrencyWallet: IResourceManager<ICurrency>, IWallet
    {
        void Change(ICurrency currency, int delta);
	    void Reset(ICurrency currency);
    }   
}