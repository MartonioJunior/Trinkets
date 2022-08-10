using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to create a manager of in-game resources.</summary>
  */
  public interface IResourceManager<T> : IResourceAdder<T>,
                                         IResourceQuantifier<T>,
                                         IResourceRemover<T>,
                                         IResourceSearcher<T>
      where T : IResource {
  }
}
