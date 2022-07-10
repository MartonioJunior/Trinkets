using UnityEngine;

namespace MartonioJunior.Trinkets.Items
{
    public abstract class ItemModel: EngineScrob, IItemModel
    {
        #region Variables
        [SerializeField] Field<IItemCategory> category = new Field<IItemCategory>();
        [SerializeField] int defaultValue;
        [SerializeField] string defaultDisplayName;
        [SerializeField] Sprite defaultDisplayIcon;
        #endregion
        #region Abstract Implementation
        public abstract void AddTo(IItemWallet wallet);
        #endregion
        #region IItemModel Implementation
        public IItemCategory Category {
            get => category.Unwrap();
            set {
                category.Set(value);
            }
        }
        public IItemModel Model => this;
        public int Value {
            get => defaultValue;
            set => defaultValue = value;
        }

        public string Name {
            get => defaultDisplayName;
            set => defaultDisplayName = value;
        }

        public Sprite Image {
            get => defaultDisplayIcon;
            set => defaultDisplayIcon = value;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"{Name}";
        }
        #endregion
    }
}