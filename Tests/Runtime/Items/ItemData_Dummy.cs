using MartonioJunior.Collectables.Items;
using UnityEngine;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemData_Dummy: ItemData
    {
        int version;

        public override int Value {
            get => version;
            set => version = value;
        }

        public override void PopulateInstance()
        {
            version++;
        }
    }
}