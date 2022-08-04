namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow checking operations inside a wallet</summary>
    */
    public interface IResourceSensor<T> where T: IWallet
    {
        /**
        <summary>Evaluates a wallet's contents.</summary>
        <param name="wallet">The wallet to be analyzed.</param>
        <returns><code>true</code> when the wallet is approved.
        <code>false</code> when the wallet is rejected.</returns>
        */
        bool Check(T wallet);
    }
}