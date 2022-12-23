namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Interface that describes a wallet of collectables.</summary>
    */
    public interface ICollectableWallet: IWallet, ICollectableOperator
    {
        
    }
    /**
    <summary>Extension class for <c>ICollectableWallet</c></summary>
    */
    public static partial class ICollectableWalletExtensions
    {
        /**
        <summary>Checks whether a collectable is part of a wallet.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="collectable">The collectable to be checked.</param>
        <returns><c>true</c> when the collectable is found.<br/>
        <c>false</c> when the collectable is not found.</returns>
        */
        public static bool Contains(this ICollectableWallet self, ICollectable collectable)
        {
            return self.AmountOf(collectable) > 0;
        }
        /**
        <summary>Adds a collectable to the wallet</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="collectables">The list of collectables to be added.</param>
        <returns>Wallet where the collectables were added in.</returns>
        */
        public static ICollectableWallet With(this ICollectableWallet self, params ICollectable[] collectables)
        {
            foreach(var collectable in collectables) {
                self.Add(new ResourceData(collectable));
            }
            return self;
        }
    }
}