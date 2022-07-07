using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets.Collectables
{
    [CreateAssetMenu(fileName = "New Collectable", menuName = "Trinkets/Collectable/Data")]
    public class CollectableData: EngineScrob, ICollectable
    {
        #region Variables
        [SerializeField] Field<ICollectableCategory> category = new Field<ICollectableCategory>();
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup()
        {
            if (category.HasValue()) {
                Category.Add(this);
            }
        }

        public override void TearDown()
        {
            if (category.HasValue()) {
                Category.Remove(this);
            }
        }
        public override void Validate() {}
        #endregion
        #region ICollectable Implementation
        public ICollectableCategory Category {
            get => category.Unwrap();
            set {
                LinkToCategory(value);
                category.Set(value);
            }
        }

        public string Name {
            get => GetName();
            set => SetName(value);
        }

        public Sprite Image {
            get => category.Unwrap()?.Image;
        }

        public int Value {
            get => GetValue();
            set => SetValue(value);
        }

        public void Collect(ICollectableWallet destination)
        {
            destination?.Add(this);
        }

        public bool WasCollectedBy(ICollectableWallet wallet)
        {
            return wallet.Contains(this);
        }
        #endregion
        #region Methods
        protected virtual string GetName()
        {
            return (category.Unwrap() as IResourceCategory)?.Name;
        }

        protected virtual int GetValue()
        {
            return 1;
        }

        private void LinkToCategory(ICollectableCategory value)
        {
            if (object.Equals(value, Category)) return;

            if (category.HasValue()) {
                Category.Remove(this);
            }

            value?.Add(this);
        }

        protected virtual void SetName(string name) {}
        protected virtual void SetValue(int value) {}
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"{name} ({(Category as IRepresentable).Name})";
        }
        #endregion
    }
}