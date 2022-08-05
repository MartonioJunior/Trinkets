namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Interface that describes a wallet of collectables.</summary>
    */
    public interface ICollectableWallet: IWallet, IResourceManager<ICollectable>, IResourceManager<ICollectableCategory>
    {
        /**
        <summary>Adds N collectables belonging to a category to the wallet.</summary>
        <param name="category">The category of which the collectables belong to.</param>
        <param name="amount">The amount of collectables to be added.</param>
        */
        void Add(ICollectableCategory category, int amount);
        /**
        <summary>Removes N collectables belonging to a category from the wallet.</summary>
        <param name="category">The category of which the collectables belong to.</param>
        <param name="amount">The amount of collectables to be removed.</param>
        */
        void Remove(ICollectableCategory category, int amount);
    }

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
    }
}