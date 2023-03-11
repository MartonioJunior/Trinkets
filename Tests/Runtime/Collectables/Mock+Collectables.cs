using MartonioJunior.Trinkets;
using MartonioJunior.Trinkets.Collectables;
using NSubstitute;
using UnityEngine;
using static Tests.Suite;

namespace Tests
{
    public static partial class Mock
    {
        #region Mock Types
        public static CollectableWallet CollectableWallet {
            get => Mock.ScriptableObject<CollectableWallet>();
        }

        public static ICollectable ICollectable {
            get {
                var collectable = Substitute<ICollectable>();
                collectable.Quantifiable.Returns(false);
                return collectable;
            }
        }

        public static CollectableCategory Category(string name)
        {
            Mock.ScriptableObject(out CollectableCategory category);
            category.Name = name;
            category.Image = Mock.Sprite();
            return category;
        }

        public static CollectableData Collectable(string name)
        {
            Mock.ScriptableObject(out CollectableData collectable);
            collectable.Name = name;
            collectable.Image = Mock.Sprite();
            return collectable;
        }
        #endregion
    }
}