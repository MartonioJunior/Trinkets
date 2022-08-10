using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface which describes representations for an object.</summary>
  */
  public interface IRepresentable {
    /**
    <summary>A textual representation for the object.</summary>
    */
    string Name { get; }
    /**
    <summary>A visual representation for the object.</summary>
    */
    Sprite Image { get; }
  }
}