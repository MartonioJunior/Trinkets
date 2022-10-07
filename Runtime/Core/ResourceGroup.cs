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
        #region IResourceGroup Implementation
        public bool Add(IResourceData data)
        {
            contents.Delta(data.Resource, data.Amount);
            return true;
        }

        public int AmountOf(IResource resource)
        {
            if (contents.TryGetValue(resource, out int amount)) {
                return amount;
            } else {
                return 0;
            }
        }

        public void Clear()
        {
            contents.Clear();
        }

        public bool Remove(IResourceData data)
        {
            IResource resource = data.Resource;
            if (!contents.TryGetValue(resource, out int amount)) return false;

            int amountToRemove = data.Amount;
            if (amount > amountToRemove) {
                contents.Delta(resource, -amountToRemove);
            } else {
                contents.Remove(resource);
            }

            return true;
        }

        public ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            var results = new List<IResourceData>();

            foreach(var (entry, amount) in contents) {
                var resourceData = new ResourceData(entry, amount);
                if (predicate?.Invoke(resourceData) ?? true) results.Add(resourceData);
            }

            return results;
        }
        #endregion
    }
}