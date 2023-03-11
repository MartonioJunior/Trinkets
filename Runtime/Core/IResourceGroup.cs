using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to describe information about a group of resources.</summary>
    */
    [MovedFrom(true, null, null, "IResourceManager<T>")]
    public interface IResourceGroup: IEnumerable<IResourceData>, IResourceAdder, IResourceQuantifier, IResourceRemover, IResourceSearcher
    {
        #region Variables
        /**
        <summary>Reveals when the group has no resources.</summary>
        <returns><c>true</c> when the group is empty.
        <c>false</c> when the group has resources.</returns>
        */
        bool IsEmpty {get;}
        #endregion
        #region Methods
        /**
        <summary>Removes all information inside a group.</summary>
        */
        void Clear();
        #endregion
    }

    /**
    <summary>Extension class for <c>IResourceGroup</c></summary>
    */
    public static partial class IResourceGroupExtensions
    {
        /**
        <summary>Combines the contents of the two existing groups
        into a new one.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="group">Other group to be used in the operation.</param>
        <returns>A new resource group.</returns>
        */
        public static IResourceGroup Join(this IResourceGroup self, IResourceGroup group)
        {
            var resultGroup = new ResourceGroup();
            resultGroup.AddRange(self);
            resultGroup.AddRange(group);

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
            var resultGroup = new ResourceGroup();

            foreach (var item in self) {
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
            if (self.IsEmpty) return false;

            group.AddRange(self);
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
            var selfNotInGroup = FilterUnique(self, group);

            resultGroup.AddRange(selfNotInGroup);

            return resultGroup;

            ICollection<IResourceData> FilterUnique(IEnumerable<IResourceData> sample, IResourceGroup group)
            {
                var resultList = new List<IResourceData>();

                foreach (var item in sample) {
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
            self.Add(data);
            return self;
        }
    }
}