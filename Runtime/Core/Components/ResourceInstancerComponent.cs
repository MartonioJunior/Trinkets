using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    public class ResourceInstancerComponent: EngineBehaviour, IResourceInstancer
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
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceInstancerComponent.OnCollected"/>
        */
        [SerializeField] UnityEvent collectedEvent;
        /**
        <summary>Event invoked when the component attempts to add
        a collectable.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action OnCollected;
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
        #region IResourceInstancer Implementation
        /**
        <inheritdoc />
        */
        public void AddTo(IResourceGroup group)
        {
            foreach(var item in Data) {
                group.Add(item);
            }

            InvokeEvents();
        }
        #endregion
        #region Methods
        /**
        <remarks>This method works the same as AddTo, but receives a
        <c>Wallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        <inheritdoc cref="ResourceInstancerComponent.AddTo(IResourceGroup)"/>
        */
        public void AddToWallet(Wallet wallet)
        {
            AddTo(wallet);
        }
        /**
        <inheritdoc />
        */
        private void InvokeEvents()
        {
            OnCollected?.Invoke();
            collectedEvent?.Invoke();
        }
        #endregion
    }
}