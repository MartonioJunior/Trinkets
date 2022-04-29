using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceQuantifier<T> where T: IResource
    {
        int AmountOf(T searchItem);
    }
}