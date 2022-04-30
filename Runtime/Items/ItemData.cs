using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    [CreateAssetMenu(fileName="NewItemData", menuName="JurassicEngine/Gameplay/ItemData")]
    public class ItemData: EngineScrob, IItem
    {
        #region Variables
        [SerializeField] string displayName;
        [SerializeField] ItemCategory[] categories;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IItem Implementation
        public string Name => displayName;
        public int Value => categories.Length;
        #endregion
    }
}