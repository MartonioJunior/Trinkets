namespace MartonioJunior.Collectables.Items
{
    public interface IItemWallet: IWallet, IResourceManager<IItem>
    {
        int AmountOf(IItemCategory category);
        void InstanceMultiple(IItem item, int amount);
        void Remove(IItemCategory category, int amount);
    }
}