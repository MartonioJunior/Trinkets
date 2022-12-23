using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to describe information about a group of resources.</summary>
    */
    [MovedFrom(true, null, null, "IResourceManager<T>")]
    public interface IResourceGroup: IResourceAdder, IResourceQuantifier, IResourceRemover, IResourceSearcher
    {
        /**
        <summary>Removes all information inside a group.</summary>
        */
        void Clear();
    }

    /**
    <summary>Extension class for <c>IResourceGroup</c></summary>
    */
    public static partial class IResourceGroupExtensions
    {
        /**
        <summary>Returns all resources present in a group.</summary>
        <param name="self">The extension object used by the operation.</param>
        */
        public static ICollection<IResourceData> All(this IResourceGroup self)
        {
            return self.Search(null);
        }
        /**
        <summary>Checks the presence of a resource inside any group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="resource">Resource to be checked.</param>
        <returns><c>true</c> when the resource is present.<br/>
        <c>false</c> when the resource is absent.</returns>
        */
        public static bool Contains(this IResourceGroup self, IResource resource)
        {
            return self.AmountOf(resource) > 0;
        }
        /**
        <summary>Combines the contents of the two existing groups
        into a new one.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="group">Other group to be used in the operation.</param>
        <returns>A new resource group.</returns>
        */
        public static IResourceGroup Join(this IResourceGroup self, IResourceGroup group)
        {
            var list = new List<IResourceData>();
            list.AddRange(self.All());
            list.AddRange(group.All());

            var resultGroup = new ResourceGroup();
            resultGroup.AddRange(list);

            return resultGroup;
        }
        /**
        <summary>Creates a new group with contents that are present
        in both groups.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="group">Other group to be used in the operation.</param>
        <returns>A new resource group.</returns>
        */
        public static IResourceGroup Overlap(this IResourceGroup self, IResourceGroup group)
        {
            var baseGroup = self.All();
            var resultGroup = new ResourceGroup();

            foreach(var item in baseGroup) {
                var resource = item.Resource;
                var amount = group.AmountOf(resource);

                if (amount > 0) {
                    resultGroup.Add(new ResourceData(resource, Mathf.Min(amount, item.Amount)));
                }
            }

            return resultGroup;
        }
        /**
        <summary>Moves the contents to another group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="group">Destination of the contents.</param>
        <returns><c>true</c> when the source group has contents.<br/>
        <c>false</c> when the source group is empty.</returns>
        */
        public static bool Transfer(this IResourceGroup self, IResourceGroup group)
        {
            var sendItems = self.All();

            if (sendItems.Count == 0) return false;

            group.AddRange(sendItems);

            self.Clear();
            return true;
        }
        /**
        <summary>Returns all of the resources that are not present in the specified group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="group">Other group to be used in the operation.</param>
        <returns>A new resource group.</returns>
        */
        public static IResourceGroup Unique(this IResourceGroup self, IResourceGroup group)
        {
            var resultGroup = new ResourceGroup();
            var selfNotInGroup = FilterUnique(self.All(), group);

            resultGroup.AddRange(selfNotInGroup);

            return resultGroup;

            ICollection<IResourceData> FilterUnique(ICollection<IResourceData> sample, IResourceGroup group)
            {
                var resultList = new List<IResourceData>();

                foreach(var item in sample) {
                    var resource = item.Resource;
                    var amount = item.Amount;
                    var groupAmount = group.AmountOf(resource);

                    if (amount > groupAmount) {
                        resultList.Add(new ResourceData(resource, amount-groupAmount));
                    }
                }

                return resultList;
            }
        }
        /**
        <summary>Insert resources into a group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="data">Resources to be added</param>
        <returns>The group where the resources were added in.</returns>
        <remarks>Useful for making multiple additions at once.</remarks>
        */
        public static IResourceGroup With(this IResourceGroup self, params IResourceData[] data)
        {
            self.AddRange(data);
            return self;
        }
    }
}