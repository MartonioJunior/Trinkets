using System;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IResourceSearcher<T> where T: IResource
    {
        T[] Search(Predicate<T> predicate);
    }
}