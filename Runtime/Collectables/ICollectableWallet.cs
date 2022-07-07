namespace MartonioJunior.Trinkets.Collectables
{
    public interface ICollectableWallet: IWallet, IResourceManager<ICollectable>, IResourceManager<ICollectableCategory>
    {
        void Add(ICollectableCategory category, int amount);
        void Remove(ICollectableCategory category, int amount);
    }

    public static partial class ICollectableWalletExtensions
    {
        public static bool Contains(this ICollectableWallet self, ICollectable collectable)
        {
            return self.AmountOf(collectable) > 0;
        }
    }
}