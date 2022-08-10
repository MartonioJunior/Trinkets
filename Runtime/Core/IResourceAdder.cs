using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to allow adding resources into an object.</summary>
  */
  public interface IResourceAdder<T>
      where T : IResource {
    /**
    <summary>Adds a resource to the object.</summary>
    <param name="resource">The resource to be added.</param>
    <returns><c>true</c> when the addition is successful.<br/>
    <c>false</c> when the addition fails.</returns>
    */
    bool Add(T resource);
  }
}
