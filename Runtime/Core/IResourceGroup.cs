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

    public static partial class IResourceGroupExtensions
    {
        /**
        <summary>Checks the presence of a resource inside any group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="resource">Resource to be checked.</param>
        <returns><c>true</c> when the resource is present.
        <c>false</c> when the resource is absent.</returns>
        */
        public static bool Contains(this IResourceGroup self, IResource resource)
        {
            return self.Search((item) => item.Resource == resource).Count > 0;
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
            list.AddRange(self.Search(null));
            list.AddRange(group.Search(null));

            var resultGroup = new ResourceGroup();
            foreach(var item in list) {
                resultGroup.Add(item);
            }

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
            var baseGroup = self.Search(null);
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
        <returns><c>true</c> when the source group has contents.
        <c>false</c> when the source group is empty.</returns>
        */
        public static bool Transfer(this IResourceGroup self, IResourceGroup group)
        {
            var sendItems = self.Search(null);

            if (sendItems.Count == 0) return false;

            foreach(var item in sendItems) {
                group.Add(item);
            }

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
            var selfNotInGroup = FilterUnique(self.Search(null), group);
            var groupNotInSelf = FilterUnique(group.Search(null), self);

            foreach(var item in selfNotInGroup) resultGroup.Add(item);
            foreach(var item in groupNotInSelf) resultGroup.Add(item);

            return resultGroup;

            ICollection<IResourceData> FilterUnique(ICollection<IResourceData> sample, IResourceGroup group)
            {
                var resultList = new List<IResourceData>();

                foreach(var item in sample) {
                    var resource = item.Resource;
                    var amount = group.AmountOf(resource);

                    if (amount == 0) {
                        resultList.Add(new ResourceData(resource, Mathf.Min(amount, item.Amount)));
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
            foreach(var item in data) {
                self.Add(item);
            }
            return self;
        }
    }
}