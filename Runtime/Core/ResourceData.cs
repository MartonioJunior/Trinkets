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
        [SerializeReference] IResource resource;
        /**
        <inheritdoc cref="IResourceData.Amount"/>
        */
        [SerializeField, Min(0f)] int amount;
        #endregion
        #region Constructors
        public ResourceData(IResource resource, int amount = 1)
        {
            this.resource = resource;
            this.amount = Mathf.Max(0, amount);
        }
        #endregion
        #region IResourceData Implementation
        /**
        <inheritdoc cref="IResourceData.Resource"/>
        */
        public IResource Resource {
            get => resource;
            set => resource = value;
        }
        /**
        <inheritdoc cref="IResourceData.Amount"/>
        */
        public int Amount {
            get => amount;
            set => amount = Mathf.Max(0, value);
        }
        #endregion
    }
}