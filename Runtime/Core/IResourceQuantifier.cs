using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceQuantifier<T> where T: IResource
    {
        int AmountOf(T searchItem);
    }
}