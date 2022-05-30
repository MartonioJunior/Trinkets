using UnityEngine;

namespace MartonioJunior.Collectables.Items
{
    public abstract class ItemData: EngineScrob, IItem
    {
        #region Variables
        [SerializeField] Sprite displayIcon;
        [SerializeField] string displayName;
        [SerializeField] Field<IItemCategory> category = new Field<IItemCategory>();
        [SerializeField] bool filterByDisplayName = false;
        string filterName;
        #endregion
        #region Abstract Methods
        public abstract int Value {get; set;}
        public abstract void PopulateInstance();
        #endregion
        #region EngineScrob Implementation
        public override void Reset() {}
        public override void Setup()
        {
            filterName = GetType().Name+"#"+displayName;
        }

        public override void TearDown() {}
        public override void Validate() {}
        #endregion
        #region IItem Implementation
        public IItemCategory Category {
            get {
                return category.Unwrap();
            } set {
                category.Set(value);
            }
        }

        public Sprite Image {
            get {
                return displayIcon;
            } set {
                displayIcon = value;
            }
        }

        public string FilterName => filterName;
        public string Name {
            get {
                return displayName;
            } set {
                displayName = value;
            }
        }

        public void InstanceOn(IItemWallet destination)
        {
            if (destination == null) return;

            var newInstance = ScriptableObject.Instantiate(this);
            newInstance.category = category;
            newInstance.PopulateInstance();
            destination.Add(newInstance);
        }

        public IItem[] GetInstancesOn(IItemWallet wallet)
        {
            return wallet.Search((item) => {
                return GetType() == item.GetType();
            });
        }
        #endregion
    }
}