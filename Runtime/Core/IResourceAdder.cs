using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceAdder<T> where T: IResource
    {
        bool Add(T resource);
    }
}