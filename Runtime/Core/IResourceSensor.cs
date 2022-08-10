namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to allow checking operations inside a wallet</summary>
  */
  public interface IResourceSensor<T>
      where T : IWallet {
    /**
    <summary>Evaluates a wallet's contents.</summary>
    <param name="wallet">The wallet to be analyzed.</param>
    <returns><c>true</c> when the wallet is approved.<br/>
    <c>false</c> when the wallet is rejected.</returns>
    */
    bool Check(T wallet);
  }
}
