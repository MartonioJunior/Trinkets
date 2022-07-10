namespace MartonioJunior.Trinkets.Items
{
    public interface IItemModel: IResource, IItemBuilder
    {
        IItemCategory Category {get;}
    }
}