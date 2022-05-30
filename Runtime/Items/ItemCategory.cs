using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    [CreateAssetMenu(fileName="New Category", menuName="Collectables/Item/Category")]
    public class ItemCategory: EngineScrob, IItemCategory
    {
        #region Constants
        public const string DefaultDisplayName = "Unnamed Category";
        #endregion
        #region Variables
        [SerializeField] string displayName;
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
        #endregion
    }
}