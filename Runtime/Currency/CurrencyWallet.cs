using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>ScriptableObject used to store in-game currencies.</summary>
    */
    [CreateAssetMenu(fileName = "New Wallet", menuName = "Trinkets/Currency/Wallet")]
    public class CurrencyWallet: Wallet, ICurrencyWallet
    {
        #region Variables
        /**
        <summary>Group which contains the wallet's resources.</summary>
        */
        [SerializeField] CurrencyGroup group = new CurrencyGroup();
        #endregion
        #region Wallet Implementation
        /**
        <inheritdoc />
        */
        public override IResourceGroup Contents => group;
        /**
        <inheritdoc cref="CurrencyGroup.Add(IResourceData)" />
        */
        public override bool Add(IResourceData data)
        {
            return group.Add(data);
        }
        /**
        <inheritdoc cref="CurrencyGroup.AmountOf(IResource)" />
        */
        public override int AmountOf(IResource resource)
        {
            return group.AmountOf(resource);
        }
        /**
        <inheritdoc />
        */
        public override void Clear()
        {
            group.Clear();
        }
        /**
        <inheritdoc cref="CollectableGroup.Remove(IResourceData)" />
        */
        public override bool Remove(IResourceData data)
        {
            return group.Remove(data);
        }
        /**
        <inheritdoc cref="CollectableGroup.Search(Predicate{IResourceData})" />
        */
        public override ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            return group.Search(predicate);
        }
        #endregion
        #region ICurrencyWallet Implementation
        /**
        <inheritdoc />
        */
        public void Change(ICurrency currency, int delta)
        {
            group.Change(currency, delta);
        }
        /**
        <inheritdoc />
        */
        public void Reset(ICurrency currency)
        {
            group.Reset(currency);
        }
        #endregion
    }
}