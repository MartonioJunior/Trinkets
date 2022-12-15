using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Generic structure used to condense information about resources.</summary>
    <remarks>Used for group operations, not recommended in most cases.</remarks>
    */
    public class ResourceGroup: IResourceGroup
    {
        #region Variables
        /**
        <summary>Contents of the group.</summary>
        */
        [SerializeField] Dictionary<IResource, int> contents = new Dictionary<IResource, int>();
        #endregion
        #region Constructors
        /**
        <summary>Creates a new Resource Group.</summary>
        */
        public ResourceGroup() {}
        /**
        <summary>Creates a new Resource Group with some elements.</summary>
        <param name="data">Collection of resources to be added in.</param>
        */
        public ResourceGroup(ICollection<ResourceData> data)
        {
            if (data == null) return;

            foreach(var entry in data) {
                Add(entry);
            }
        }
        #endregion
        #region IResourceGroup Implementation
        /**
        <inheritdoc />
        */
        public bool Add(IResourceData data)
        {
            IResource resource = data.Resource;
            int amountToAdd = data.Amount;

            if (resource == null || amountToAdd <= 0) return false;

            contents.Delta(resource, amountToAdd);
            return true;
        }
        /**
        <inheritdoc />
        */
        public int AmountOf(IResource resource)
        {
            if (resource == null || !contents.TryGetValue(resource, out int amount))
                return 0;

            return amount;
        }
        /**
        <inheritdoc />
        */
        public void Clear()
        {
            contents.Clear();
        }
        /**
        <inheritdoc />
        */
        public bool Remove(IResourceData data)
        {
            IResource resource = data.Resource;
            int amountToRemove = data.Amount;

            if (!contents.TryGetValue(resource, out int amount) || amountToRemove <= 0)
                return false;

            if (amount > amountToRemove) {
                contents.Delta(resource, -amountToRemove);
            } else {
                contents.Remove(resource);
            }

            return true;
        }
        /**
        <inheritdoc />
        */
        public ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            var results = new List<IResourceData>();

            foreach(var pair in contents) {
                var resourceData = new ResourceData(pair.Key, pair.Value);
                if (predicate?.Invoke(resourceData) ?? true) results.Add(resourceData);
            }

            return results;
        }
        #endregion
    }
}