using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    [CreateAssetMenu(fileName = "New Wallet", menuName = "Trinkets/Item/Wallet")]
    public class ItemWallet: EngineScrob, IItemWallet
    {
        #region Variables
        [SerializeField] Dictionary<IItemModel, List<IItem>> contents = new Dictionary<IItemModel, List<IItem>>();
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IItemWallet Implementation
        public bool Add(IItem resource)
        {
            if (resource == null) return false;

            IItemModel key = resource.Model;

            if (!contents.ContainsKey(key)) {
                contents[key] = new List<IItem>();
            }

            contents[key].Add(resource);
            return true;
        }

        public int AmountOf(IItem searchItem)
        {
            if (searchItem == null) return 0;

            IItemModel key = searchItem.Model;
            return AmountOf(key);
        }

        public int AmountOf(IItemCategory category)
        {
            if (category == null) return 0;

            int amountCount = 0;
            foreach(var pair in contents) {
                if (pair.Key.Category == category) {
                    amountCount += pair.Value.Count;
                }
            }
            return amountCount;
        }

        public int AmountOf(IItemModel model)
        {
            if (model == null) return 0;

            if (!contents.ContainsKey(model)) {
                return 0;
            } else {
                return contents[model].Count;
            } 
        }

        public void Clear()
        {
            contents.Clear();
        }

        public void CopyMultiple(IItem item, int amount)
        {
            if (item == null) return;

            for(int i = 0; i < amount; i++) {
                Add(item.Copy());
            }
        }

        public void InstanceMultiple(IItemModel model, int amount)
        {
            if (model == null) return;

            for(int i = 0; i < amount; i++) {
                model.AddTo(this);
            }
        }

        public bool Remove(IItem resource)
        {
            IItemModel itemType = resource.Model;

            if (contents.TryGetValue(itemType, out List<IItem> list)) {
                return RemoveItemOnList(list, resource);
            } else {
                return false;
            }
        }

        private bool RemoveItemOnList(List<IItem> list, IItem item)
        {
            return list.Remove(item);
        }

        public void Remove(IItemCategory category, int amount)
        {
            foreach(var pair in contents) {
                if (pair.Key.Model.Category != category) continue;

                var itemList = pair.Value;
                for(int i = itemList.Count-1; i >= 0; i--) {
                    if (amount <= 0) return;

                    itemList.RemoveAt(i);
                    amount--;
                }
            }
        }

        public void Remove(IItemModel model, int amount)
        {
            if (model == null) return;

            if (contents.TryGetValue(model, out var itemList)) {
                for(int i = itemList.Count-1; i >= 0; i--) {
                    if (amount <= 0) return;

                    itemList.RemoveAt(i);
                    amount--;
                }
            }
        }

        public IItem[] Search(Predicate<IItem> predicate)
        {
            var resultList = new List<IItem>();

            foreach(var itemList in contents.Values) {
                if (predicate == null) {
                    resultList.AddRange(itemList);
                } else foreach(var item in itemList) {
                    if (predicate(item)) resultList.Add(item);
                }
            }

            return resultList.ToArray();
        }

        public IItem[] SearchOn(IItemModel model, Predicate<IItem> predicate)
        {
            var resultList = new List<IItem>();
            if (model == null) return resultList.ToArray();

            var itemList = contents[model];
            if (predicate == null) {
                resultList.AddRange(itemList);
            } else foreach(var item in itemList) {
                if (predicate(item)) resultList.Add(item);
            }

            return resultList.ToArray();
        }
        #endregion
        #region Methods
        public string DescribeContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append("\n");

            foreach(var pair in contents) {
                sb.Append(GetDescription((pair.Key as IRepresentable).Name, pair.Value));
            }
            return sb.ToString();
        }

        private string GetDescription(string modelName, List<IItem> items)
        {
            if (items.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(modelName);
            sb.Append(": ");

            foreach(var item in items) {
                sb.Append(item.ToString());
                sb.Append(" | ");
            }
            sb.Append("\n");
            return sb.ToString();
        }
        #endregion
    }
}