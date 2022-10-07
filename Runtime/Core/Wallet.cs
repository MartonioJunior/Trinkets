using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public abstract class Wallet: EngineScrob, IWallet
    {
        #region Abstract
        public abstract IResourceGroup Contents { get; }
        public abstract bool Add(IResourceData data);
        public abstract int AmountOf(IResource resource);
        public abstract void Clear();
        public abstract bool Remove(IResourceData data);
        public abstract ICollection<IResourceData> Search(Predicate<IResourceData> predicate);
        #endregion
        #region EngineScrob Implementation
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        #endregion
    }
}