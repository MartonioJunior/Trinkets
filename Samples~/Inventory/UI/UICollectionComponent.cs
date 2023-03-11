using UnityEngine;

namespace Sample.Inventory
{
    /**
    <summary>Component responsible for managing a set of repeating views.</summary>
    */
    [AddComponentMenu("UI/UI Collection")]
    public class UICollectionComponent: MonoBehaviour
    {
        #region Interfaces
        public interface IDataSource
        {
            #region Methods
            public int NumberOfElementsFor(UICollectionComponent component);
            public void Populate(UICollectionComponent component, int index);
            #endregion
        }

        public interface IPool
        {
            #region Methods
            public UIDisplay CellFor(UICollectionComponent component, int index);
            #endregion
        }
        #endregion
        #region Variables
        [SerializeField] GameObject fallbackCell;
        #endregion
        #region Properties
        public IDataSource DataSource {get; set;}
        public IPool Pool {get; set;}
        #endregion
        #region MonoBehaviour Lifecycle
        public void Validate()
        {
            UIDisplay.ValidatePrefab(ref fallbackCell);
        }
        #endregion
        #region Methods
        private void AddInstances(int amount)
        {
            for (int i = 1; i < amount; i++) Instance();
        }

        public UIDisplay FetchCell(int index)
        {
            var cellObject = transform.GetChild(index);
            return cellObject?.GetComponent<UIDisplay>(); 
        }

        public UIDisplay Instance()
        {
            if (Pool != null) {
                return Pool.CellFor(this, transform.childCount);
            } else {
                var gameObject = GameObject.Instantiate(fallbackCell, transform);
                return gameObject.GetComponent<UIDisplay>();
            }
        }

        private void RemoveInstances(int amount)
        {
            foreach (Transform child in transform) {
                if (amount-- <= 0) return;
                GameObject.Destroy(child.gameObject);
            }
        }

        [ContextMenu("Refresh Collection")]
        public void Refresh()
        {
            if (DataSource == null) return;

            int newAmount = DataSource.NumberOfElementsFor(this);
            Resize(newAmount);

            for (int i = 0; i < newAmount; i++) {
                DataSource.Populate(this, i);
            }
        }

        public void Resize(int newAmount)
        {
            var currentAmount = transform.childCount;

            if (currentAmount < newAmount) {
                AddInstances(newAmount - currentAmount + 1);
            } else if (currentAmount > newAmount) {
                RemoveInstances(currentAmount - newAmount);
            }
        }
        #endregion
    }
}