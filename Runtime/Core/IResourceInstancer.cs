using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to describe an object capable of inserting
    resources into a group.</summary>
    */
    public interface IResourceInstancer
    {
        /**
        <summary>Inserts resources into a group.</summary>
        <param name="group">The receiving group.</param>
        */
        void AddTo(IResourceGroup group);
    }
}