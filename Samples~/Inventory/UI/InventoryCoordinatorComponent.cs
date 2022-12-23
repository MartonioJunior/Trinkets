using UnityEngine;
using UnityEngine.InputSystem;

namespace Sample.Inventory
{
    public class InventoryCoordinatorComponent: MonoBehaviour
    {
        #region Variables
        [Header("UI References")]
        [SerializeField] GameObject inventoryPanel;
        [SerializeField] UICollectionComponent collection;
        #endregion
        #region Properties
        public bool InventoryIsOpen => inventoryPanel.activeSelf;
        #endregion
        #region MonoBehaviour Lifecycle
        void Awake()
        {
            CloseMenu();
        }

        void Start()
        {
            collection.Refresh();
        }
        #endregion
        #region Methods
        public void ToggleInventoryMenu(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;

            if (InventoryIsOpen) {
                CloseMenu();
            } else {
                OpenMenu();
            }
        }

        private void OpenMenu()
        {
            inventoryPanel.SetActive(true);
            collection.Refresh();
        }

        private void CloseMenu()
        {
            inventoryPanel.SetActive(false);
        }
        #endregion
    }
}