using System;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Structure used to represent quantities for resources.</summary>
    */
    [Serializable]
    public struct ResourceData: IResourceData
    {
        #region Variables
        /**
        <inheritdoc cref="IResourceData.Resource" />
        */
        [SerializeField] Resource _resource;
        /**
        <remarks>Serves as a backup for when the object doesn't inherit from Resource.</remarks>
        <inheritdoc cref="IResourceData.Resource" />
        */
        [SerializeReference] IResource resource;
        /**
        <inheritdoc cref="IResourceData.Amount" />
        */
        [SerializeField, Min(0f)] int amount;
        #endregion
        #region Constructors
        /**
        <summary>Initializes a new ResourceData structure.</summary>
        <param name="item">In-game resource.</param>
        <param name="amount">Quantity.</param>
        */
        public ResourceData(IResource item, int amount = 1)
        {
            if (item is Resource r) {
                _resource = r;
                resource = null;
            } else {
                resource = item;
                _resource = null;
            }
            
            this.amount = (item?.Quantifiable ?? true) ? Mathf.Max(0, amount) : 1;
        }
        #endregion
        #region IResourceData Implementation
        /**
        <inheritdoc cref="IResourceData.Resource" />
        */
        public IResource Resource {
            get => Get();
            set => SetResource(value);
        }
        /**
        <inheritdoc cref="IResourceData.Amount" />
        */
        public int Amount {
            get => amount;
            set => SetAmount(value);
        }
        #endregion
        #region Methods
        /**
        <summary>Captures the correct resource reference to use.</summary>
        <returns>The resource reference to be used.</returns>
        */
        private IResource Get()
        {
            if (_resource != null) {
                return _resource;
            } else {
                return resource;
            }
        }
        /**
        <summary>Sets the amount of a resource inside the ResourceData.</summary>
        <param name="amount">The amount received to set the parameter.</param>
        */
        private void SetAmount(int amount)
        {
            if (IsQuantifiable(Get())) {
                this.amount = Mathf.Max(0, amount);
            } else {
                this.amount = 1;
            }
        }
        /**
        <summary>Sets the correct reference based on the resource type.</summary>
        <param name="item">Value to be set.</param>
        */
        private void SetResource(IResource item)
        {
            if (item is Resource r) {
                _resource = r;
                resource = null;
            } else {
                resource = item;
                _resource = null;
            }

            if (!IsQuantifiable(item)) {
                amount = 1;
            }
        }
        /**
        <summary>Checks if a resource can be counted.</summary>
        <param name="item">Resource to be checked.</param>
        <returns><c>true</c> when the resource can be counted.<br/>
        <c>false</c> when the resource cannot be counted.</returns>
        */
        private bool IsQuantifiable(IResource item)
        {
            return item?.Quantifiable ?? true;
        }
        #endregion
        #region Operators
        public static ResourceData operator +(ResourceData lhs, int rhs) => new ResourceData(lhs.Resource, lhs.Amount + rhs);
        public static ResourceData operator -(ResourceData lhs, int rhs) => new ResourceData(lhs.Resource, lhs.Amount - rhs);
        public static ResourceData operator *(ResourceData lhs, int rhs) => new ResourceData(lhs.Resource, lhs.Amount * rhs);
        public static ResourceData operator /(ResourceData lhs, int rhs) => new ResourceData(lhs.Resource, lhs.Amount / rhs);
        #endregion
    }
}