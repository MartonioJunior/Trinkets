using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    [AddComponentMenu("Trinkets/Resource Drainer")]
    public class ResourceDrainerComponent: EngineBehaviour, IResourceTaxer
    {
        #region Variables
        /**
        <summary>List of resources to be removed.</summary>
        */
        [field: SerializeField] public List<ResourceData> Data {get; private set;} = new List<ResourceData>();
        /**
        <summary>Where the resources drained will be placed.</summary>
        <remarks></remarks>
        */
        [field: SerializeField] public Wallet Destination {get; set;}
        #endregion
        #region Events
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceInstancerComponent.OnDrain"/>
        */
        [SerializeField] UnityEvent drained;
        /**
        <summary>Event invoked when the component attempts to remove a collectable
        from a group.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action OnDrain;
        #endregion
        #region EngineBehaviour Implementation
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        #endregion
        #region IResourceTaxer Implementation
        /**
        <inheritdoc />
        */
        public void Tax(IResourceGroup group)
        {
            if (!enabled) return;

            foreach(var item in Data) {
                if (group.Remove(item)) {
                    Destination.Add(item);
                }
            }

            InvokeEvents();
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
        /**
        <inheritdoc />
        */
        private void InvokeEvents()
        {
            OnDrain?.Invoke();
            drained?.Invoke();
        }
        #endregion
    }
}