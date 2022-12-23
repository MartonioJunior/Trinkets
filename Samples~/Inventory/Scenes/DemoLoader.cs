using MartonioJunior.Trinkets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Sample.Inventory
{
    public class DemoLoader: MonoBehaviour
    {
        #region Variables
        [SerializeField] Wallet wallet;
        #endregion
        #region MonoBehaviour Lifecycle
        void Awake()
        {
            LoadInventoryUI();
        }

        void Update()
        {
            if (Keyboard.current.rKey.wasPressedThisFrame) {
                Reload();
            }
        }
        #endregion
        #region Methods
        public void Reload()
        {
            wallet?.Clear();
            SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
        }

        private void LoadInventoryUI()
        {
            SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);   
        }
        #endregion
    }
}