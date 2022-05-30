using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    [CreateAssetMenu(fileName="New Wallet", menuName="Collectables/Item/Wallet")]
    public class ItemWallet: EngineScrob, IItemWallet
    {
        #region Variables
        [SerializeField] Dictionary<string, List<IItem>> contents = new Dictionary<string, List<IItem>>();
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

            string itemType = resource.FilterName;

            if (!contents.ContainsKey(itemType)) {
                contents[itemType] = new List<IItem>();
            }

            contents[itemType].Add(resource);
            return true;
        }

        public int AmountOf(IItem searchItem)
        {
            if (searchItem == null) return 0;

            string itemType = searchItem.FilterName;

            if (!contents.ContainsKey(itemType)) {
                return 0;
            } else {
                return contents[itemType].Count;
            }
        }

        public int AmountOf(IItemCategory category)
        {
            if (category == null) return 0;

            int amountCount = 0;

            foreach(var itemList in contents.Values) {
                foreach(var item in itemList) {
                    if (item.Category == category) amountCount++;
                }
            }

            return amountCount;
        }

        public void Clear()
        {
            contents.Clear();
        }

        public void InstanceMultiple(IItem item, int amount)
        {
            if (item == null) return;

            for(int i = 0; i < amount; i++) {
                item.InstanceOn(this);
            }
        }

        public bool Remove(IItem resource)
        {
            string itemType = resource.FilterName;

            if (contents.TryGetValue(itemType, out List<IItem> list)) {
                return RemoveItemOnList(list, resource);
            } else {
                return false;
            }
        }

        private bool RemoveItemOnList(List<IItem> list, IItem item)
        {
            if (list.Contains(item)) {
                return list.Remove(item);
            }

            for(int i = 0; i < list.Count; i++) {
                var resource = list[i];
                if (resource.FilterName == item.FilterName) {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public void Remove(IItemCategory category, int amount)
        {
            foreach(var itemList in contents.Values) {
                for(int i = itemList.Count-1; i >= 0; i--) {
                    if (amount <= 0) return;

                    var item = itemList[i];
                    if (item.Category == category) {
                        itemList.RemoveAt(i);
                        amount--;
                    }
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
        #endregion
    }
}