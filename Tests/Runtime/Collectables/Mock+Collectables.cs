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
                Engine.Instance(out CollectableWallet wallet);
                objectList.Add(wallet);
                return wallet;
            }
        }

        public CollectableCategory Category(string name)
        {
            Engine.Instance(out CollectableCategory category);
            category.Name = name;
            category.Image = this.Sprite;
            objectList.Add(category);
            return category;
        }

        public CollectableData Collectable(string name)
        {
            Engine.Instance(out CollectableData collectable);
            collectable.Name = name;
            collectable.Image = this.Sprite;
            objectList.Add(collectable);
            return collectable;
        }
        #endregion
    }
}