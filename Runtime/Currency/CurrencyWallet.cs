using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using CurrencyLoadout = System.Collections.Generic.Dictionary<MartonioJunior.Trinkets.Currency.ICurrency, int>;

namespace MartonioJunior.Trinkets.Currency
{
    [CreateAssetMenu(fileName = "NewWallet", menuName = "Trinkets/Currency/Wallet")]
    public class CurrencyWallet: EngineScrob, ICurrencyWallet
    {
        #region Variables
        [SerializeField] CurrencyLoadout currencyAmounts = new CurrencyLoadout();
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region ICurrencyWallet Implementation
        public bool Add(ICurrency currency)
        {
            if (currency == null || currencyAmounts.ContainsKey(currency)) return false;

            currencyAmounts[currency] = 0;
            return true;
        }

        public int AmountOf(ICurrency currency)
        {
            if (currency != null && currencyAmounts.TryGetValue(currency, out int value)) return value;
            else return 0;
        }

        public void Change(ICurrency currency, int delta)
        {
            currencyAmounts.Delta(currency, delta);
        }

        public void Clear()
        {
            currencyAmounts.Clear();
        }

        public bool Remove(ICurrency currency)
        {
            if (currency == null) return false;

            return currencyAmounts.Remove(currency);
        }

        public void Reset(ICurrency currency)
        {
            if (currency == null) return;

            currencyAmounts[currency] = 0;
        }

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