using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Structure used to store resources.</summary>
    */
    public abstract class Wallet: ScriptableObject, IWallet
    {
        #region Abstract
        /**
        <inheritdoc />
        */
        public abstract IResourceGroup Contents { get; }
        /**
        <inheritdoc />
        */
        public abstract bool Add(IResourceData data);
        /**
        <inheritdoc />
        */
        public abstract int AmountOf(IResource resource);
        /**
        <inheritdoc />
        */
        public abstract void Clear();
        /**
        <inheritdoc />
        */
        public abstract bool Remove(IResourceData data);
        /**
        <inheritdoc />
        */
        public abstract ICollection<IResourceData> Search(Predicate<IResourceData> predicate);
        #endregion
    }
}