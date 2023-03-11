using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component that inserts resources into resource groups and wallets.</summary>
    */
    [AddComponentMenu("Trinkets/Resource Instancer")]
    public class ResourceInstancerComponent: MonoBehaviour, IResourceInstancer
    {
        #region Variables
        /**
        <summary>The list of resources to be given out.</summary>
        */
        [field: SerializeField] public List<ResourceData> Data {get; private set;} = new List<ResourceData>();
        /**
        <summary>Wallet where the resources will come from.</summary>
        <remarks>If a wallet is not supplied, the component assumes an
        infinite amount of resources.</remarks>
        */
        [field: SerializeField] public Wallet Source {get; set;}
        #endregion
        #region Events
        /**
        <summary>Event invoked when the component attempts to add a collectable.</summary>
        */
        [Header("Events")]
        public Event OnCollected;
        #endregion
        #region IResourceInstancer Implementation
        /**
        <inheritdoc />
        */
        public void AddTo(IResourceGroup group)
        {
            if (!enabled) return;

            if (Source == null) {
                group.AddRange(Data);
            } else foreach (var item in Data) {
                if (Source.Remove(item)) {
                    group.Add(item);
                }
            }

            OnCollected.Invoke();
        }
        #endregion
        #region Methods
        /**
        <remarks>This method works the same as AddTo, but receives a
        <c>Wallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        <inheritdoc cref="ResourceInstancerComponent.AddTo(IResourceGroup)" />
        */
        public void AddToWallet(Wallet wallet)
        {
            AddTo(wallet);
        }
        #endregion
    }
}