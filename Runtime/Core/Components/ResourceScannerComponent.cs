using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    [AddComponentMenu("Trinkets/Resource Scanner")]
    public class ResourceScannerComponent: MonoBehaviour, IResourceScanner
    {
        #region Variables
        /**
        <inheritdoc cref="CollectableScanner.TaxGroupOnScan" />
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
        <summary>Event invoked when the component scans a resource group.</summary>
        <remarks>Sends in the result of the scan as a parameter.</remarks>
        */
        [Header("Events")]
        public Event<bool> OnScan;
        /**
        <summary>Event invoked when the component removes elements from a resource group.</summary>
        */
        public Event OnTax;
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
            if (!enabled) return false;

            foreach(var item in Data) {
                if (group.AmountOf(item.Resource) < item.Amount) return false;
            }

            return true;
        }
        /**
        <summary>Checks whether a group fulfills the specified criteria
        of a <cref>IResourceScanner</cref></summary>
        <param name="group">The group to be scanned.</param>
        <returns><c>true</c> when the group passes a scan.<br/>
        <c>false</c> when the group does not fulfill the criteria.</returns>
        */
        public bool Scan(IResourceGroup group)
        {
            bool scanResult = IResourceScannerExtensions.Scan(this, group);
            OnScan.Invoke(scanResult);
            return scanResult;
        }
        /**
        <inheritdoc />
        */
        public void Tax(IResourceGroup group)
        {
            if (!enabled) return;

            group.RemoveRange(Data);

            OnTax.Invoke();
        }
        #endregion
        #region Methods
        /**
        <summary>Scans a specified wallet.</summary>
        <param name="wallet">The wallet to be scanned.</param>
        <remarks>Uses a <c>Wallet</c> instead to allow for use with events in the Unity inspector.</remarks>
        */
        public void ScanWallet(Wallet wallet)
        {
            Scan(wallet);
        }
        /**
        <remarks>Uses a <c>Wallet</c> instead to allow for use with events in the Unity inspector.</remarks>
        <inheritdoc cref="ResourceScannerComponent.Tax(IResourceGroup)" />
        */
        public void TaxWallet(Wallet wallet)
        {
            Tax(wallet);
        }
        #endregion
    }
}