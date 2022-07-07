using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    [CreateAssetMenu(fileName = "New Category", menuName = "Trinkets/Item/Category")]
    public class ItemCategory: EngineScrob, IItemCategory
    {
        #region Constants
        public const string DefaultDisplayName = "Unnamed Category";
        #endregion
        #region Variables
        [SerializeField] string displayName;
        [SerializeField] Sprite displayIcon;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate()
        {
            if (string.IsNullOrEmpty(displayName)) {
                displayName = DefaultDisplayName;
            }
        }
        #endregion
        #region IItemCategory Implementation
        public string Name {
            get => displayName;
            set {
                displayName = value;
                Validate();
            }
        }

        public Sprite Image {
            get => displayIcon;
            set => displayIcon = value;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"{displayName} (Item Category)";
        }
        #endregion
    }
}