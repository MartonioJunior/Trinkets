namespace MartonioJunior.Trinkets.Items
{
    public interface IItemModel: IItemBuilder
    {
        IItemCategory Category {get;}
    }
}