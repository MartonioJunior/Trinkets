using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceManager<T>: IResourceAdder<T>, IResourceQuantifier<T>, IResourceRemover<T>, IResourceSearcher<T> where T: IResource
    {
        
    }
}