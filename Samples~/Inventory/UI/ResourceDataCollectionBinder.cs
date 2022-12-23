using System.Collections.Generic;
using MartonioJunior.Trinkets;
using UnityEngine;

namespace Sample.Inventory
{
    public class ResourceDataCollectionBinder: UICollectionBinder
    {
        #region Variables
        [SerializeField] Wallet wallet;
        [SerializeField] GameObject displayCell;
        IResourceData[] temp;
        #endregion
        #region MonoBehaviour Lifecycle
        void OnEnable()
        {
            temp = (IResourceData[]) wallet.Search(null);
        }

        void OnValidate()
        {
            UIDisplay.ValidatePrefab(ref displayCell);
        }
        #endregion
        #region UICollectionBinder Implementation
        public override UIDisplay CellFor(UICollectionComponent component, int index)
        {
            return Instance(displayCell);
        }

        public override int NumberOfElementsFor(UICollectionComponent component)
        {
            return temp.Length;
        }

        public override void Populate(UICollectionComponent component, int index)
        {
            if (!(component.FetchCell(index) is ItemCellDisplay cell)) return;

            cell.Set(temp[index]);
        }
        #endregion
    }
}