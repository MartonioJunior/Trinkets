namespace MartonioJunior.Trinkets
{
    public interface IResourceSensor<T> where T: IWallet
    {
        bool Check(T wallet);
    }
}