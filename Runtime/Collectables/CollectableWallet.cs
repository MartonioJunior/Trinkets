using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    [CreateAssetMenu(fileName = "NewWallet", menuName = "Trinkets/Collectable/Wallet")]
    public class CollectableWallet: EngineScrob, ICollectableWallet
    {
        #region Variables
        List<ICollectable> nullCategoryCollectables = new List<ICollectable>();
        Dictionary<ICollectableCategory, List<ICollectable>> contents = new Dictionary<ICollectableCategory, List<ICollectable>>();
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region ICollectableWallet Implementation
        public bool Add(ICollectable collectable)
        {
            ICollectableCategory category = collectable.Category;

            if (category == null) {
                if (nullCategoryCollectables.Contains(collectable)) {
                    return false;
                } else {
                    nullCategoryCollectables.Add(collectable);
                    return true;
                }
            }

            if (!contents.ContainsKey(category)) {
                contents[category] = new List<ICollectable>();
            } else if (contents[category].Contains(collectable)) {
                return false;
            }

            contents[category].Add(collectable);
            return true;
        }

        public bool Add(ICollectableCategory resource)
        {
            bool wasSuccessful = false;
            foreach (var item in resource?.Search(null)) {
                if (Add(item)) wasSuccessful = true;
            }
            return wasSuccessful;
        }

        public void Add(ICollectableCategory category, int amount)
        {
            if (amount <= 0) return;

            foreach (var item in category?.Search(null)) {
                if (Add(item)) amount--;
                if (amount <= 0) break;
            }
        }

        public int AmountOf(ICollectable searchItem)
        {
            ICollectableCategory category = searchItem.Category;

            bool foundInDictionary = category != null && contents.ContainsKey(category) && contents[category].Contains(searchItem);
    
            if (foundInDictionary || nullCategoryCollectables.Contains(searchItem)) {
                return 1;
            } else {
                return 0;
            }
        }

        public int AmountOf(ICollectableCategory searchItem)
        {
            if (searchItem == null) {
                return nullCategoryCollectables.Count;
            } else if (!contents.ContainsKey(searchItem)) {
                return 0;
            } else {
                return contents[searchItem].Count;
            }
        }

        public void Clear()
        {
            contents.Clear();
            nullCategoryCollectables.Clear();
        }

        public ICollectable[] Search(Predicate<ICollectable> predicate)
        {
            var resultList = new List<ICollectable>();

            resultList.AddRange(ListSearch(predicate, nullCategoryCollectables));
            foreach(var list in contents.Values) {
                resultList.AddRange(ListSearch(predicate, list));
            }

            return resultList.ToArray();
        }

        public ICollectableCategory[] Search(Predicate<ICollectableCategory> predicate)
        {
            var resultList = new List<ICollectableCategory>();

            if (predicate == null) {
                resultList.AddRange(contents.Keys);
            } else foreach(var category in contents.Keys) {
                if (predicate(category)) resultList.Add(category);
            }

            return resultList.ToArray();
        }

        public bool Remove(ICollectable collectable)
        {
            ICollectableCategory category = collectable.Category;

            if (category == null) {
                return nullCategoryCollectables.Remove(collectable);
            } else if (!contents.ContainsKey(category)) {
                return false;
            }

            return contents[category].Remove(collectable);
        }

        public bool Remove(ICollectableCategory resource)
        {
            return contents.Remove(resource);
        }

        public void Remove(ICollectableCategory category, int amount)
        {
            if (!contents.ContainsKey(category)) return;

            var list = contents[category];
            
            if (amount >= list.Count) {
                list.Clear();
                return;
            }

            for(int i = 0; i < amount; i++) {
                if (list.Count == 0) break;
                list.RemoveAt(0);
            }
        }
        #endregion
        #region Methods
        public bool Contains(ICollectable collectable)
        {
            ICollectableCategory category = collectable?.Category;

            if (category == null) {
                return nullCategoryCollectables.Contains(collectable);
            } else if (!contents.ContainsKey(category)) {
                return false;
            }

            return contents[category].Contains(collectable);
        }

        public ICollectable[] ListSearch(Predicate<ICollectable> predicate, List<ICollectable> list)
        {
            if (predicate == null) return list.ToArray();

            var resultList = new List<ICollectable>();

            foreach(var item in list) {
                if (predicate(item)) resultList.Add(item);
            }

            return resultList.ToArray();
        }
        #endregion
    }
}