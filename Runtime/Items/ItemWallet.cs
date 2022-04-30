using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    [CreateAssetMenu(fileName="NewItemWallet", menuName="JurassicEngine/Gameplay/ItemWallet")]
    public class ItemWallet: EngineScrob, IWallet
    {
        #region Variables
        [SerializeField] Dictionary<Type, List<IItem>> items;
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IWallet Implementation
        public void Clear()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Methods
        public void Add(IItem item, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void AddXOfEachFor(ItemCategory category, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IItem item, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAllFrom(ItemCategory category)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}