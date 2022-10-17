using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Class used to represent a group of in-game currencies</summary>
    */
    public class CurrencyGroup: IResourceGroup, ICurrencyOperator
    {
        #region Variables
        /**
        <summary>Collection responsible to store the amounts for each currency.</summary>
        */
        [SerializeField] Dictionary<ICurrency, int> contents = new Dictionary<ICurrency, int>();
        #endregion
        #region IResourceGroup Implementation
        /**
        <summary>Adds a currency to the group.</summary>
        <inheritdoc />
        */
        public bool Add(IResourceData data)
        {
            if (!(data?.Resource is ICurrency currency) || data.Amount <= 0) return false;
            
            if (contents.TryGetValue(currency, out int amount)) {
                contents[currency] = amount + data.Amount;
            } else {
                contents.Add(currency, data.Amount);
            }

            return true;
        }
        /**
        <summary>Checks how much of a currency is on a resource group.</summary>
        <param name="resource">The currency to be checked.</param>
        <inheritdoc />
        */
        public int AmountOf(IResource resource)
        {
            if (resource is ICurrency currency && contents.TryGetValue(currency, out int value)) {
                return value;
            } else {
                return 0;
            }
        }
        /**
        <inheritdoc />
        */
        public void Clear()
        {
            contents.Clear();
        }
        /**
        <summary>Removes a currency from the group.</summary>
        <remarks>When a currency is removed via this method, it does not
        appear on the database.</remarks>
        <inheritdoc />
        */
        public bool Remove(IResourceData data)
        {
            int amountToRemove = data.Amount;
            if (!(data.Resource is ICurrency currency) || amountToRemove <= 0) return false;

            if (contents.TryGetValue(currency, out int amount)) {
                contents[currency] = Mathf.Max(amount - amountToRemove, 0);
                return true;
            } else {
                return false;
            }
        }
        /**
        <summary>Searches for currencies inside a group.</summary>
        <remarks>When predicate is null, all currencies present in the group
        are returned. If you want to just remove the amount of a currency,
        use the <c>Reset</c> method instead</remarks>
        <inheritdoc />
        */
        public ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            var list = new List<IResourceData>();

            foreach(var pair in contents) {
                var data = new ResourceData(pair.Key, pair.Value);
                if (predicate?.Invoke(data) ?? true) list.Add(data);
            }

            return list.ToArray();
        }
        #endregion
        #region ICurrencyOperator Implementation
        /**
        <inheritdoc />
        */
        public void Change(ICurrency currency, int delta)
        {
            if (contents.TryGetValue(currency, out var amount)) {
                contents[currency] = Mathf.Max(amount+delta, 0);
            } else {
                contents[currency] = Mathf.Max(delta, 0);
            }
        }
        /**
        <remarks>Maintains the currency as a key in the group, still appearing
        when using the <c>DescribeContents</c> method.</remarks>
        <inheritdoc />
        */
        public void Reset(ICurrency currency)
        {
            if (currency == null) return;

            contents[currency] = 0;
        }
        #endregion
        #region Methods
        /**
        <summary>Describes the list of currencies present in the
        group.</summary>
        <returns>A string describing the contents of the group by
        category.</returns>
        */
        [Obsolete]
        public string DescribeContents()
        {
            StringBuilder sb = new StringBuilder();
            if (contents.Count == 0) {
                sb.Append("Empty");
            } else foreach(var pair in contents) {
                sb.Append(pair.Value);
                sb.Append("(");
                sb.Append(pair.Key.Symbol);
                sb.Append(") | ");
            }
            return sb.ToString();
        }
        #endregion
    }
}