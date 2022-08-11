// #define ENABLE_INTERFACE_FIELDS
using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>ScriptableObject which defines a collectable.</summary>
  */
  [CreateAssetMenu(fileName = "New Collectable",
                   menuName = "Trinkets/Collectable/Data")]
  public class CollectableData : EngineScrob, ICollectable {
#region Variables
    /**
    <summary>The description of the collectable.</summary>
    */
    [SerializeField]
    string displayName;
    /**
    <summary>The icon of the collectable.</summary>
    */
    [SerializeField]
    Sprite displayImage;
    /**
    <inheritdoc cref="ICollectable.Category"/>
    */
#if ENABLE_INTERFACE_FIELDS
    [SerializeField]
    Field<ICollectableCategory> category = new Field<ICollectableCategory>();
#else
    [SerializeField]
    CollectableCategory category;
#endif
#endregion
#region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup() {
#if ENABLE_INTERFACE_FIELDS
      if (category.HasValue())
#else
      if (Category != null)
#endif
        Category.Add(this);
    }
    /**
    <inheritdoc />
    */
    public override void TearDown() {
#if ENABLE_INTERFACE_FIELDS
      if (category.HasValue())
#else
      if (Category != null)
#endif
        Category.Remove(this);
    }
    /**
    <inheritdoc />
    */
    public override void Validate() {}
#endregion
#region ICollectable Implementation
#if ENABLE_INTERFACE_FIELDS
    /**
    <inheritdoc />
    */
    public ICollectableCategory Category {
      get => category.Unwrap();
      set {
        LinkToCategory(value);
        category.Set(value);
      }
#else
    /**
    <inheritdoc cref="ICollectable.Category"/>
    */
    public CollectableCategory Category {
      get => category;
      set {
        LinkToCategory(value);
        category = value;
      }
    }

    ICollectableCategory ICollectable.Category {
      get => Category;
#endif
    }
    /**
    <inheritdoc />
    */
    public string Name {
      get => GetName();
      set => SetName(value);
    }
    /**
    <inheritdoc />
    */
    public Sprite Image {
      get => GetImage();
      set => SetImage(value);
    }
    /**
    <inheritdoc />
    */
    public int Value {
      get => GetValue();
      set => SetValue(value);
    }
    /**
    <inheritdoc />
    */
    public void Collect(ICollectableWallet destination) {
      destination?.Add(this);
    }
    /**
    <inheritdoc />
    */
    public bool WasCollectedBy(ICollectableWallet wallet) {
      return wallet.Contains(this);
    }
#endregion
#region Methods
    /**
    <summary>Returns the image of the collectable.</summary>
    */
    protected virtual Sprite GetImage() {
      if (displayImage != null)
        return displayImage;
      else
        return Category?.Image;
    }
    /**
    <summary>Returns the name of the collectable.</summary>
    */
    protected virtual string GetName() {
      if (string.IsNullOrEmpty(displayName))
        return $"{name} ({Category?.Name ?? "No Category"})";
      else
        return displayName;
    }
    /**
    <summary>Returns the value of the collectable.</summary>
    */
    protected virtual int GetValue() { return 1; }
    /**
    <summary>Handles the linking of a category to the collectable.</summary>
    <param name="value">The category to be linked.</param>
    <remarks>If the collectable already belonged to a category, it is removed
    from the original category and the category is replaced by the new one.
    </remarks>
    */
    private void LinkToCategory(ICollectableCategory value) {
      if (object.Equals(value, Category))
        return;
#if ENABLE_INTERFACE_FIELDS
      if (category.HasValue())
#else
      if (Category != null)
#endif
        Category.Remove(this);

      value?.Add(this);
    }
    /**
    <summary>Sets the icon of the collectable.</summary>
    */
    protected virtual void SetImage(Sprite sprite) { displayImage = sprite; }
    /**
    <summary>Sets the name of the collectable.</summary>
    */
    protected virtual void SetName(string name) { displayName = name; }
    /**
    <summary>Sets the value of the collectable.</summary>
    */
    protected virtual void SetValue(int value) {}
#endregion
#region Methods
    public override string ToString() { return $"{name}({Category?.Name})"; }
#endregion
  }
}