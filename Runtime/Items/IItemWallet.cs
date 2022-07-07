using System;

namespace MartonioJunior.Trinkets.Items
{
    public interface IItemWallet: IWallet, IResourceManager<IItem>
    {
        int AmountOf(IItemCategory category);
        int AmountOf(IItemModel model);
        void CopyMultiple(IItem item, int amount);
        void InstanceMultiple(IItemModel model, int amount);
        void Remove(IItemCategory category, int amount);
        void Remove(IItemModel model, int amount);
        IItem[] SearchOn(IItemModel model, Predicate<IItem> predicate);
    }
}