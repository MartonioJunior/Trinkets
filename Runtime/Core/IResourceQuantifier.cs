using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to determine the amount of resources contained
    in an object.</summary>
    */
    public interface IResourceQuantifier
    {
        /**
        <summary>Checks the amount of a resource inside.</summary>
        <param name="resource">The resource to check for.</param>
        <returns>The amount of resource available.</returns>
        */
        int AmountOf(IResource resource);
    }

    public static partial class IResourceQuantifierExtensions
    {
        /**
        <summary>Checks the presence of a resource inside any group.</summary>
        <param name="self">The extension object used by the operation.</param>
        <param name="resource">Resource to be checked.</param>
        <returns><c>true</c> when the resource is present.<br/>
        <c>false</c> when the resource is absent.</returns>
        */
        public static bool Contains(this IResourceQuantifier self, IResource resource)
        {
            return self.AmountOf(resource) > 0;
        }

        public static bool Contains(this IResourceQuantifier self, IResourceData resourceData)
        {
            return self.AmountOf(resourceData.Resource) >= resourceData.Amount;
        }

        public static bool Contains(this IResourceQuantifier self, params IResourceData[] resources)
        {
            return Contains(self, 1, resources);
        }

        public static bool Contains(this IResourceQuantifier self, int multiplier, params IResourceData[] resources)
        {
            foreach(var item in resources) {
                if (self.AmountOf(item.Resource) < item.Amount * multiplier) return false;
            }

            return true;
        }
    }
}