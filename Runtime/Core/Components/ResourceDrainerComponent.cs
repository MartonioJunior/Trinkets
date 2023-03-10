using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Event = MartonioJunior.Trinkets.Event;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component that removes resources from resource groups and wallets.</summary>
    */
    [AddComponentMenu("Trinkets/Resource Drainer")]
    public class ResourceDrainerComponent: MonoBehaviour, IResourceTaxer
    {
        #region Variables
        /**
        <summary>List of resources to be removed.</summary>
        */
        [field: SerializeField] public List<ResourceData> Data {get; private set;} = new List<ResourceData>();
        /**
        <summary>Where the resources drained will be placed.</summary>
        <remarks>If no Wallet is supplied, the resources will just be discarded.</remarks>
        */
        [field: SerializeField] public Wallet Destination {get; set;}
        #endregion
        #region Events
        /**
        <summary>Event invoked when the component attempts to remove a collectable from a group.</summary>
        */
        [Header("Events")]
        public Event OnDrain;
        #endregion
        #region IResourceTaxer Implementation
        /**
        <inheritdoc />
        */
        public void Tax(IResourceGroup group)
        {
            if (!enabled) return;

            if (Destination == null) {
                group.RemoveRange(Data);
            } else foreach(var item in Data) {
                if (group.Remove(item)) {
                    Destination.Add(item);
                }
            }

            OnDrain.Invoke();
        }
        #endregion
        #region Methods
        /**
        <param name="wallet">The wallet that'll have resources taken away.</param>
        <inheritdoc cref="ResourceDrainerComponent.Tax(IResourceGroup)" />
        */
        public void Drain(Wallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}