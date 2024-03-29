using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Defines a category of collectable, used as a reference point to mark collectables.</summary>
    */
    [CreateAssetMenu(fileName = "New Category", menuName = "Trinkets/Collectable/Category")]
    public class CollectableCategory: Resource, ICollectableCategory
    {
        #region Constants
        /**
        <summary>Default Name used when the name of a <c>CollectableCategory</c> is empty or null.</summary>
        */
        public const string DefaultCategoryName = "Unnamed Category";
        #endregion
        #region IResource Implementation
        /**
        <inheritdoc />
        */
        public override int Value {
            get => 1;
            set {}
        }
        /**
        <inheritdoc />
        */
        public override string DefaultName => DefaultCategoryName;
        /**
        <inheritdoc />
        */
        public override Sprite DefaultImage => null;
        /**
        <inheritdoc />
        */
        public override bool Quantifiable => true;
        #endregion
        #region Methods
        /**
        <summary>Returns a visual description of the category</summary>
        <returns>A string containing the category's name with the identifier "(Collectable Category)".</returns>
        <example>A <c>CollectableCategory</c> named "Lollipop" returns "Lollipop (Collectable Category)"</example>
        */
        public override string ToString()
        {
            return $"{Name} (Collectable Category)";
        }
        #endregion
    }
}