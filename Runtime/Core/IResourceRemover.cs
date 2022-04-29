using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceRemover<T> where T: IResource
    {
        bool Remove(T resource);
    }
}