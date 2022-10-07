using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>ScriptableObject which defines a collectable.</summary>
    */
    [CreateAssetMenu(fileName = "New Collectable", menuName = "Trinkets/Collectable/Data")]
    public class CollectableData: Resource, ICollectable
    {
        #region Constants
        /**
        <summary>Default name used in the absence of a name for the 
        category it belongs to.</summary>
        */
        public const string EmptyCategory = "No Category";
        #endregion
        #region Variables
        /**
        <summary>The description of the collectable.</summary>
        */
        [SerializeField] string displayName;
        /**
        <summary>The icon of the collectable.</summary>
        */
        [SerializeField] Sprite displayImage;
        /**
        <inheritdoc cref="ICollectable.Category"/>
        */
        [SerializeField] CollectableCategory category;
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
        #region Resource Implementation
        /**
        <inheritdoc />
        */
        public override string DefaultName => $"{name} ({Category?.Name ?? EmptyCategory})";
        /**
        <inheritdoc />
        */
        public override Sprite DefaultImage => Category?.Image;
        /**
        <inheritdoc />
        */
        public override string Name {
            get => string.IsNullOrEmpty(displayName) ? DefaultName : $"{displayName} ({Category?.Name ?? EmptyCategory})";
            set => displayName = value;
        }
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
        public override bool Quantifiable => false;
        #endregion
        #region ICollectable Implementation
        /**
        <inheritdoc />
        */
        public ICollectableCategory Category {
            get => category;
            set {
                if (value is CollectableCategory cat) category = cat;
            }
        }
        /**
        <inheritdoc />
        */
        public void Collect(ICollectableWallet destination)
        {
            destination?.Add((ResourceData)this);
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
        public override string ToString()
        {
            return $"{name}({Category?.Name})";
        }
        #endregion
        #region Operators
        public static implicit operator ResourceData(CollectableData data)
        {
            return new ResourceData(data);
        }
        #endregion
    }
}