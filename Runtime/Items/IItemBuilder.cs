namespace MartonioJunior.Trinkets.Items
{
    public interface IItemBuilder: IResourceInstancer<IItemWallet>
    {
        IItemModel Model {get;}
    }
}