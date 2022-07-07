using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceRemover<T> where T: IResource
    {
        bool Remove(T resource);
    }
}