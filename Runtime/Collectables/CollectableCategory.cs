using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Defines a category of collectable, used as a reference point to mark
  collectables.</summary>
  */
  [CreateAssetMenu(fileName = "New Category",
                   menuName = "Trinkets/Collectable/Category")]
  public class CollectableCategory : EngineScrob, ICollectableCategory {
#region Constants
    /** <summary>Default Name used when the name of a
    <c>CollectableCategory</c> is empty or null.</summary>
    */
    public const string DefaultDisplayName = "Unnamed Category";
#endregion
#region Variables
    /**
    <inheritdoc cref="IRepresentable.Image"/>
    */
    [SerializeField]
    Sprite displayIcon;
    /**
    <inheritdoc cref="IRepresentable.Name"/>
    */
    [SerializeField]
    string displayName;
    /**
    <summary>List of collectables that belong to the category.</summary>
    <remarks> Currently only receives initialized collectables. </remarks>
    */
    [SerializeField, HideInInspector]
    List<ICollectable> elements = new List<ICollectable>();
    /**
    <summary>Determines the total worth of the collectables stored by the
    category.</summary>
    */
    int valueCount;
#endregion
#region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup() {}
    /**
    <inheritdoc />
    */
    public override void TearDown() {}
    /**
    <inheritdoc />
    */
    public override void Validate() {
      if (string.IsNullOrEmpty(displayName)) {
        displayName = DefaultDisplayName;
      }
    }
#endregion
#region IResource Implementation
    /**
    <inheritdoc />
    */
    public int Value => valueCount;
#endregion
#region IResourceCategory Implementation
    /**
    <inheritdoc />
    */
    public string Name {
      get => displayName;
      set {
        displayName = value;
        Validate();
      }
    }
#endregion
#region ICollectableCategory Implementation
    /**
    <inheritdoc />
    */
    public Sprite Image {
      get => displayIcon;
      set => displayIcon = value;
    }
    /**
    <summary>Adds a collectable to the category</summary>
    <param name="collectable">The collectable to be added.</param>
    <inheritdoc />
    */
    public bool Add(ICollectable collectable) {
      if (elements.Contains(collectable))
        return false;

      elements.Add(collectable);
      valueCount += collectable.Value;
      return true;
    }
    /**
    <summary>Returns the number of collectables linked to a category.</summary>
    <param name="collectable">The collectable to search for.</param>
    <returns><c>0</c> when the collectable is not linked to the category.<br/>
    <c>1</c> when the collectable is linked to the category.</returns>
    <inheritdoc />
    */
    public int AmountOf(ICollectable collectable) {
      return Contains(collectable) ? 1 : 0;
    }
    /**
    <summary>Searches for resources linked to the category.</summary>
    <returns>An array of collectables.</returns>
    <inheritdoc />
    */
    public ICollectable[] Search(Predicate<ICollectable> predicate) {
      if (predicate == null)
        return elements.ToArray();

      var resultList = new List<ICollectable>();
      foreach (var item in elements) {
        if (predicate(item))
          resultList.Add(item);
      }
      return resultList.ToArray();
    }
    /**
    <summary>Removes a collectable from the category.</summary>
    <param name="collectable">The collectable to be removed.</param>
    <inheritdoc />
    */
    public bool Remove(ICollectable collectable) {
      var wasInCollection = elements.Remove(collectable);
      if (wasInCollection)
        valueCount -= collectable.Value;
      return wasInCollection;
    }
#endregion
#region Methods
    /**
    <summary>Removes all collectables from a category.</summary>
    */
    public void Clear() {
      elements.Clear();
      valueCount = 0;
    }
    /**
    <summary>Checks whether a collectable belongs to a category.</summary>
    <param name="collectable">The collectable to check.</param>
    <returns><c>true</c> when the collectable is in a Category. <c>false</c>
    when it's not.</returns>
    */
    public bool Contains(ICollectable collectable) {
      return elements.Contains(collectable);
    }
    /**
    <summary>Returns a visual description of the category</summary>
    <returns>The category's name"</returns>
    <example>A <c>CollectableCategory</c> named "Lollipop" returns
    "Lollipop (Collectable Category)"</example>
    */
    public override string ToString() {
      return $"{displayName} (Collectable Category)";
    }
#endregion
  }
}
