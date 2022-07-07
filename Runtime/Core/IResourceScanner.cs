namespace MartonioJunior.Trinkets
{
    public interface IResourceScanner<T>: IResourceSensor<T>, IResourceTaxer<T> where T: IWallet
    {
        bool TaxWalletOnScan {get; set;}
    }

    public static partial class IResourceScannerExtensions
    {
        public static bool Scan<T>(this IResourceScanner<T> self, T wallet) where T: IWallet
        {
            bool checkIsValid = self.Check(wallet);
            if (checkIsValid && self.TaxWalletOnScan) {
                self.Tax(wallet);
            }
            return checkIsValid;
        }
    }
}