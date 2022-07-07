using MartonioJunior.Trinkets.Items;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemProcessor_Dummy: ItemProcessor<Item_Dummy, int>
    {
        public override int Convert(Item_Dummy item)
        {
            return item.Value;
        }

        public override void Reset() {}
        public override void Setup() {}
        public override void TearDown() {}
        public override void Validate() {}
    }
}