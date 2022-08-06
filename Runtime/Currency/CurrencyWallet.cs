using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Currency
{
    /**
    <summary>ScriptableObject used to store in-game currencies.</summary>
    */
    [CreateAssetMenu(fileName = "NewWallet", menuName = "Trinkets/Currency/Wallet")]
    public class CurrencyWallet: EngineScrob, ICurrencyWallet
    {
        #region Variables
        /**
        <summary>Collection responsible to store the amounts for each currency.</summary>
        */

        [SerializeField] Dictionary<ICurrency, int> currencyAmounts = new Dictionary<ICurrency, int>();
        #endregion
        #region EngineScrob Implementation
        /**
        <inheritdoc />
        */
        public override void Reset() {}
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        /**
        <inheritdoc />
        */
        public override void Validate() {}
        #endregion
        #region ICurrencyWallet Implementation
        /**
        <summary>Adds a currency to the wallet.</summary>
        <remarks>When a currency is added via this method, the initial amount is
        set at zero. To add a currency with a initial amount, use the <c>Change</c>
        method instead.</remarks>
        <inheritdoc />
        */
        public bool Add(ICurrency currency)
        {
            if (currency == null || currencyAmounts.ContainsKey(currency)) return false;

            currencyAmounts[currency] = 0;
            return true;
        }
        /**
        <summary>Checks how much of a currency is on a wallet.</summary>
        <param name="currency">The currency to be checked.</param>
        <inheritdoc />
        */
        public int AmountOf(ICurrency currency)
        {
            if (currency != null && currencyAmounts.TryGetValue(currency, out int value)) return value;
            else return 0;
        }
        /**
        <inheritdoc />
        */
        public void Change(ICurrency currency, int delta)
        {
            currencyAmounts.Delta(currency, delta);
        }
        /**
        <inheritdoc />
        */
        public void Clear()
        {
            currencyAmounts.Clear();
        }
        /**
        <summary>Removes a currency from the wallet.</summary>
        <param name="currency">The currency to be removed.</param>
        <remarks>When a currency is removed via this method, it does not
        appear on the database.</remarks>
        <inheritdoc />
        */
        public bool Remove(ICurrency currency)
        {
            if (currency == null) return false;

            return currencyAmounts.Remove(currency);
        }
        /**
        <remarks>Maintains the currency as a key in the wallet, still appearing
        when using the <c>DescribeContents</c> method.</remarks>
        <inheritdoc />
        */
        public void Reset(ICurrency currency)
        {
            if (currency == null) return;

            currencyAmounts[currency] = 0;
        }
        /**
        <summary>Searches for currencies inside a wallet</summary>
        <remarks>When predicate is null, all currencies present in the wallet
        are returned. If you want to just remove the amount of a currency,
        use the <c>Reset</c> method instead</remarks>
        <inheritdoc />
        */
        public ICurrency[] Search(Predicate<ICurrency> predicate)
        {
            var list = new List<ICurrency>();

            if (predicate == null) {
                list.AddRange(currencyAmounts.Keys);
            } else foreach(var pair in currencyAmounts) {
                var currency = pair.Key;
                if (predicate(currency)) list.Add(currency);
            }

            return list.ToArray();
        }
        #endregion
        #region Methods
        /**
        <summary>Describes the list of currencies present in the
        wallet.</summary>
        <returns>A string describing the contents of the wallet by
        category.</returns>
        */
        public string DescribeContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append(": ");

            if (currencyAmounts.Count == 0) {
                sb.Append("Empty");
            } else foreach(var pair in currencyAmounts) {
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