namespace MartonioJunior.Collectables
{
    public interface IResourceSensor<T> where T: IWallet
    {
        bool Check(T wallet);
    }
}