using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Collectables.Collectables
{
    [CreateAssetMenu(fileName="New Collectable", menuName="Collectables/Collectable/Data")]
    public class CollectableData: EngineScrob, ICollectable
    {
        #region Variables
        [SerializeField] Field<ICollectableCategory> category;
        #endregion
        #region Events
        [SerializeField] UnityEvent<CollectableData> wasCollected;
        [SerializeField] UnityEvent<CollectableData> wasRecollected;
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
        public ICollectableCategory Category => category.Unpack();

        public string Name {
            get => GetName();
            set => SetName(value);
        }

        public int Value => GetValue();

        public void Collect(ICollectableWallet destination)
        {
            destination.Add(this);
        }

        public bool WasCollectedBy(ICollectableWallet wallet)
        {
            return wallet.Contains(this);
        }
        #endregion
        #region Methods
        protected virtual string GetName()
        {
            return (category.Unpack() as IResourceCategory).Name;
        }

        protected virtual int GetValue()
        {
            return 1;
        }

        protected virtual void SetName(string name) {}
        protected virtual void SetValue(int value) {}
        #endregion
    }
}