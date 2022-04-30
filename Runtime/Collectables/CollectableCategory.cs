using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Collectables.Collectables
{
    [CreateAssetMenu(fileName = "New Category", menuName = "Collectables/Collectable/Category")]
    public class CollectableCategory: EngineScrob, ICollectableCategory
    {
        #region Variables
        [SerializeField] string displayName;
        [SerializeField] List<ICollectable> elements = new List<ICollectable>();
        int valueCount;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IResource Implementation
        public int Value => valueCount;
        #endregion
        #region IResourceCategory Implementation
        public string Name => name;
        #endregion
        #region ICollectableCategory Implementation
        public bool Add(ICollectable element)
        {
            if (elements.Contains(element)) return false;

            elements.Add(element);
            valueCount += element.Value;
            return true;
        }

        public int AmountOf(ICollectable searchItem)
        {
            return Contains(searchItem) ? 1 : 0;
        }

        public ICollectable[] Search(System.Predicate<ICollectable> predicate)
        {
            var resultList = new List<ICollectable>();
            foreach (var item in elements) {
                if (predicate(item)) resultList.Add(item);
            }
            return resultList.ToArray();
        }

        public bool Remove(ICollectable element)
        {
            var wasInCollection = elements.Remove(element);
            if (wasInCollection) valueCount -= element.Value;
            return wasInCollection;
        }
        #endregion
        #region Methods
        public void Clear()
        {
            elements.Clear();
        }

        public bool Contains(ICollectable element)
        {
            return elements.Contains(element);
        }
        #endregion
    }
}