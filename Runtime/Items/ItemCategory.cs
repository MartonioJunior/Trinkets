using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    [CreateAssetMenu(fileName="NewItemCategory", menuName="JurassicEngine/Gameplay/ItemCategory")]
    public class ItemCategory: EngineScrob, IResourceCategory
    {
        #region Variables
        [SerializeField] string displayName;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IResourceCategory Implementation
        public string Name => displayName;
        #endregion
    }
}