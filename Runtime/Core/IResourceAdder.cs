using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceAdder<T> where T: IResource
    {
        bool Add(T resource);
    }
}