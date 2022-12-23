using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Interface which describes all operations only possible with collectables.</summary>
    */
    public interface ICollectableOperator
    {
        #region Methods
        /**
        <summary>Adds N collectables belonging to a group.</summary>
        <param name="group">The group of which the collectables belong to.</param>
        <param name="amount">The amount of collectables to be added.</param>
        <returns>Returns the number of collectables added successfully.</returns>
        */
        int AddFrom(CollectableGroup group, int amount);
        /**
        <summary>Removes N collectables belonging to a group.</summary>
        <param name="group">The group of which the collectables belong to.</param>
        <param name="amount">The amount of collectables to be removed.</param>
        <returns>Returns the number of collectables removed successfully.</returns>
        */
        int RemoveFrom(CollectableGroup group, int amount);
        #endregion
    }
}