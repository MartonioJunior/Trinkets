using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
/**
<summary>ScriptableObject which defines a collectable.</summary>
*/
[CreateAssetMenu(fileName = "New Collectable", menuName = "Trinkets/Collectable/Data")]
public class CollectableData: EngineScrob, ICollectable
{
    #region Variables
    /**
    <inheritdoc cref="ICollectable.Category"/>
    */
    [SerializeField] Field<ICollectableCategory> category = new Field<ICollectableCategory>();
    #endregion
    #region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup()
    {
        if (category.HasValue()) {
            Category.Add(this);
        }
    }
    /**
    <inheritdoc />
    */
    public override void TearDown()
    {
        if (category.HasValue()) {
            Category.Remove(this);
        }
    }
    /**
    <inheritdoc />
    */
    public override void Validate() {}
    #endregion
    #region ICollectable Implementation
    /**
    <inheritdoc />
    */
    public ICollectableCategory Category {
        get => category.Unwrap();
        set {
            LinkToCategory(value);
            category.Set(value);
        }
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
        get => category.Unwrap()?.Image;
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
    public void Collect(ICollectableWallet destination)
    {
        destination?.Add(this);
    }
    /**
    <inheritdoc />
    */
    public bool WasCollectedBy(ICollectableWallet wallet)
    {
        return wallet.Contains(this);
    }
    #endregion
    #region Methods
    /**
    <summary>Returns the name of the collectable.</summary>
    */
    protected virtual string GetName()
    {
        return (category.Unwrap() as IResourceCategory)?.Name;
    }
    /**
    <summary>Returns the value of the collectable.</summary>
    */
    protected virtual int GetValue()
    {
        return 1;
    }
    /**
    <summary>Handles the linking of a category to the collectable.</summary>
    <param name="value">The category to be linked.</param>
    <remarks>If the collectable already belonged to a category, it is removed
    from the original category and the category is replaced by the new one.
    </remarks>
    */
    private void LinkToCategory(ICollectableCategory value)
    {
        if (object.Equals(value, Category)) return;

        if (category.HasValue()) {
            Category.Remove(this);
        }

        value?.Add(this);
    }
    /**
    <summary>Sets the name of the collectable.</summary>
    */
    protected virtual void SetName(string name) {}
    /**
    <summary>Sets the value of the collectable.</summary>
    */
    protected virtual void SetValue(int value) {}
    #endregion
    #region Methods
    public override string ToString()
    {
        return $"{name}({Name})";
    }
    #endregion
}
}