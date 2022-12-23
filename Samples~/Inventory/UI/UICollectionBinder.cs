using MartonioJunior.Core;
using UnityEngine;

namespace Sample.Inventory
{
    [RequireComponent(typeof(UICollectionComponent))]
    public abstract class UICollectionBinder: MonoBehaviour, UICollectionComponent.IDataSource, UICollectionComponent.IPool
    {
        #region Variables
        UICollectionComponent uiCollection;
        #endregion
        #region MonoBehaviour Lifecycle
        void Awake()
        {
            uiCollection = GetComponent<UICollectionComponent>();
            uiCollection.Pool = this;
            uiCollection.DataSource = this;
        }

        void OnDestroy()
        {
            uiCollection.Pool = null;
            uiCollection.DataSource = null;
            uiCollection = null;
        }
        #endregion
        #region UICollectionComponent.IDataSource Implementation
        public abstract int NumberOfElementsFor(UICollectionComponent component);
        public abstract void Populate(UICollectionComponent component, int index);
        #endregion
        #region UICollectionComponent.IPool Implementation
        public abstract UIDisplay CellFor(UICollectionComponent component, int index);
        #endregion
        #region Methods
        public UIDisplay Instance(GameObject prefab)
        {
            var gameObject = GameObject.Instantiate(prefab, transform);
            return gameObject.GetComponent<UIDisplay>();
        }
        #endregion
    }
}