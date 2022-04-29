using System;
using UnityEngine;

namespace MartonioJunior.Collectables
{
    public interface IResourceSearcher<T> where T: IResource
    {
        T[] Search(Predicate<T> predicate);
    }
}