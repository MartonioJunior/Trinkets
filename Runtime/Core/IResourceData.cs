using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to describe information about a resource.</summary>
    */
    public interface IResourceData
    {
        #region Properties
        /**
        <summary>The resource referenced.</summary>
        */
        IResource Resource {get; set;}
        /**
        <summary>The quantity of the resource.</summary>
        */
        int Amount {get; set;}
        #endregion
    }
}