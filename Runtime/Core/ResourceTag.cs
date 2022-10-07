using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public class ResourceTag: EngineScrob, IResourceTag
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
        #region EngineScrob Implementation
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        #endregion
        #region IResource Implementation
        public string Name {
            get => displayName;
            set => displayName = value;
        }
        /**
        <inheritdoc cref="IRepresentable.Image"/>
        */
        public Sprite Image {
            get => displayIcon;
            set => displayIcon = value;
        }
        #endregion
    }
}