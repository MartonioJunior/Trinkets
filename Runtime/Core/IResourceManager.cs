using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceManager<T>: IResourceAdder<T>, IResourceQuantifier<T>, IResourceRemover<T>, IResourceSearcher<T> where T: IResource
    {
        
    }
}