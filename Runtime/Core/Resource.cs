using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public abstract class Resource: EngineScrob, IResource
    {
        #region Variables
        /**
        <inheritdoc cref="IRepresentable.Image"/>
        */
        [SerializeField] string displayName;
        /**
        <inheritdoc cref="IRepresentable.Name"/>
        */
        [SerializeField] Sprite displayIcon;
        #endregion
        #region Abstract
        /**
        <summary>The name to be used when <c>displayName</c> is null or empty.</summary>
        */
        public abstract string DefaultName {get;}
        /**
        <summary>The sprite to use when <c>displayIcon</c> is null</summary>
        */
        public abstract Sprite DefaultImage {get;}
        /**
        <inheritdoc cref="IResource.Value"/>
        */
        public abstract int Value { get; set; }
        /**
        <inheritdoc cref="IResource.Quantifiable"/>
        */
        public abstract bool Quantifiable { get; }
        #endregion
        #region IResource Implementation
        /**
        <inheritdoc cref="IRepresentable.Name"/>
        */
        public virtual string Name {
            get => string.IsNullOrEmpty(displayName) ? DefaultName : displayName;
            set => displayName = value;
        }
        /**
        <inheritdoc cref="IRepresentable.Image"/>
        */
        public Sprite Image {
            get => displayIcon != null ? displayIcon : DefaultImage;
            set => displayIcon = value;
        }
        #endregion
    }
}