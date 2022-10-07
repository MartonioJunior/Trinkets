using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public partial class Mock
    {
        #region Mock Types
        public CollectableWallet CollectableWallet {
            get {
                EngineScrob.Instance(out CollectableWallet wallet);
                objectList.Add(wallet);
                return wallet;
            }
        }

        public CollectableCategory Category(string name)
        {
            EngineScrob.Instance(out CollectableCategory category);
            category.Name = name;
            category.Image = this.Sprite;
            objectList.Add(category);
            return category;
        }

        public CollectableData Collectable(string name)
        {
            EngineScrob.Instance(out CollectableData collectable);
            collectable.Name = name;
            collectable.Image = this.Sprite;
            objectList.Add(collectable);
            return collectable;
        }
        #endregion
    }
}