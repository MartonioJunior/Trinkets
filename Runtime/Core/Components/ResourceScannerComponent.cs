using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    public class ResourceScannerComponent: EngineBehaviour, IResourceScanner
    {
        #region Variables
        /**
        <inheritdoc cref="CollectableScanner.TaxGroupOnScan"/>
        */
        [SerializeField] bool taxWallet;
        /**
        <summary>List of resource requirements for the Scan operation.</summary>
        */
        [field: SerializeField] public List<ResourceData> Data {get; private set;} = new List<ResourceData>();
        /**
        <summary>Wallet where the resources collected by scanning
        will be stored.</summary>
        <remarks>If no wallet is supplied, the resources will be discarded.</remarks>
        */
        [field: SerializeField] public Wallet Destination {get; set;}
        #endregion
        #region Events
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceScannerComponent.OnScan"/>
        */
        [SerializeField] UnityEvent<bool> scanned;
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceScannerComponent.OnTax"/>
        */
        [SerializeField] UnityEvent taxed;
        /**
        <summary>Event invoked when the component scans a resource group.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action<bool> OnScan;
        /**
        <summary>Event invoked when the component removes elements from a resource group.
        </summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action OnTax;
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
        #region IResourceScanner Implementation
        /**
        <inheritdoc />
        */
        public bool TaxGroupOnScan {
            get => taxWallet;
            set => taxWallet = value;
        }
        /**
        <inheritdoc />
        */
        public bool Check(IResourceGroup group)
        {
            foreach(var item in Data) {
                if (group.AmountOf(item.Resource) < item.Amount) return false;
            }

            return true;
        }
        /**
        <inheritdoc />
        */
        public void Tax(IResourceGroup group)
        {
            foreach(var item in Data) {
                group.Remove(item);
            }
        }
        #endregion
        #region Methods
        /**
        <summary>Scans a specified wallet.</summary>
        <param name="wallet">The wallet to be scanned.</param>
        <remarks>Uses a <c>Wallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        */
        public void Scan(Wallet wallet)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, wallet);
            OnScan?.Invoke(scanResult);
        }
        /**
        <remarks>Uses a <c>Wallet</c> instead to allow for use with events
        in the Unity inspector.</remarks>
        <inheritdoc cref="ResourceScannerComponent.Tax(IResourceGroup)"/>
        */
        public void TaxWallet(Wallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}