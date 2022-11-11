using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Object used to categorize resources in a game.</summary>
    */
    public class ResourceTag: ScriptableObject, IResourceTag
    {
        #region Variables
        /**
        <inheritdoc cref="IRepresentable.Image" />
        */
        [SerializeField] string displayName;
        /**
        <inheritdoc cref="IRepresentable.Name" />
        */
        [SerializeField] Sprite displayIcon;
        #endregion
        #region IResource Implementation
        public string Name {
            get => displayName;
            set => displayName = value;
        }
        /**
        <inheritdoc cref="IRepresentable.Image" />
        */
        public Sprite Image {
            get => displayIcon;
            set => displayIcon = value;
        }
        #endregion
    }
}