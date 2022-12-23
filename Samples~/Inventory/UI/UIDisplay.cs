using UnityEngine;

namespace Sample.Inventory
{
    public abstract class UIDisplay: MonoBehaviour
    {
        #region Variables
        public RectTransform RectTransform {get; private set;}
        #endregion
        #region MonoBehaviour Lifecycle
        void Awake()
        {
            RectTransform = transform as RectTransform;
        }
        #endregion
        #region Methods
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        #endregion
        #region Static Methods
        public static bool ValidatePrefab(GameObject prefab)
        {
            if (prefab.GetComponent<UIDisplay>() == null) {
                Debug.LogWarning($"Prefab {prefab.name} is not a UI Display. Please add the UI Display component and try again!");
                return false;
            } else {
                return true;
            }
        }

        public static void ValidatePrefab(ref GameObject prefab)
        {
            if (!UIDisplay.ValidatePrefab(prefab)) {
                prefab = null;
            }
        }
        #endregion
    }
}