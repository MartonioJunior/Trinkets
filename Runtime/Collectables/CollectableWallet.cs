using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Class used to store in-game collectables.</summary>
    */
    [CreateAssetMenu(fileName = "New Wallet", menuName = "Trinkets/Collectable/Wallet")]
    public class CollectableWallet: Wallet, ICollectableWallet
    {
        #region Variables
        /**
        <summary>Group responsible for handling the wallet's contents.</summary>
        */
        [SerializeField] CollectableGroup group = new CollectableGroup();
        #endregion
        #region Wallet Implementation
        /**
        <inheritdoc />
        */
        public override IResourceGroup Contents => group;
        /**
        <inheritdoc cref="CollectableGroup.Add(IResourceData)"/>
        */
        public override bool Add(IResourceData data)
        {
            return group.Add(data);
        }
        /**
        <inheritdoc cref="CollectableGroup.AmountOf(IResource)"/>
        */
        public override int AmountOf(IResource resource)
        {
            return group.AmountOf(resource);
        }
        /**
        <inheritdoc cref="CollectableGroup.Clear"/>
        */
        public override void Clear()
        {
            group.Clear();
        }
        /**
        <inheritdoc cref="CollectableGroup.Remove(IResourceData)"/>
        */
        public override bool Remove(IResourceData data)
        {
            return group.Remove(data);
        }
        /**
        <inheritdoc cref="CollectableGroup.Search(Predicate{IResourceData})"/>
        */
        public override ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            return group.Search(predicate);
        }
        #endregion
        #region ICollectableWallet Implementation
        /**
        <inheritdoc />
        */
        public int AddFrom(CollectableGroup group, int amount)
        {
            return group.AddFrom(group, amount);
        }
        /**
        <inheritdoc />
        */
        public int RemoveFrom(CollectableGroup group, int amount)
        {
            return group.RemoveFrom(group, amount);
        }
        #endregion
    }
}