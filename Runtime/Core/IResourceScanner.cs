namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used for scanning resources into a wallet</summary>
    */
    public interface IResourceScanner<T>: IResourceSensor<T>, IResourceTaxer<T> where T: IWallet
    {
        /**
        <summary>Checks whether a scanner is allowed to tax the scanned resources.
        <c>true</c>: Scan removes the resources from a wallet.
        <c>false</c>: Scan maintains the wallet's resources intact.</summary>
        */
        bool TaxWalletOnScan {get; set;}
    }

    public static partial class IResourceScannerExtensions
    {
        /**
        <summary>Checks whether a wallet fulfills the specified criteria
        of a <cref>IResourceScanner</cref></summary>
        <param name="wallet">The wallet to be scanned.</param>
        <returns><code>true</code> when the wallet passes a scan.
        <code>false</code> when the wallet does not fulfill the criteria.</returns>
        */
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