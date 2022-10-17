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
        <inheritdoc cref="IResourceData.Resource"/>
        */
        [SerializeField] Resource _resource;
        /**
        <inheritdoc cref="IResourceData.Resource"/>
        */
        [SerializeField] IResource resource;
        /**
        <inheritdoc cref="IResourceData.Amount"/>
        */
        [SerializeField, Min(0f)] int amount;
        #endregion
        #region Constructors
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
        <inheritdoc cref="IResourceData.Resource"/>
        */
        public IResource Resource {
            get => Get();
            set => SetResource(value);
        }
        /**
        <inheritdoc cref="IResourceData.Amount"/>
        */
        public int Amount {
            get => amount;
            set => SetAmount(value);
        }
        #endregion
        #region Methods
        private IResource Get()
        {
            if (_resource != null) {
                return _resource;
            } else {
                return resource;
            }
        }

        private void SetAmount(int amount)
        {
            if (IsQuantifiable(Get())) {
                this.amount = Mathf.Max(0, amount);
            } else {
                this.amount = 1;
            }
        }

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

        private bool IsQuantifiable(IResource item)
        {
            return item?.Quantifiable ?? true;
        }
        #endregion
    }
}