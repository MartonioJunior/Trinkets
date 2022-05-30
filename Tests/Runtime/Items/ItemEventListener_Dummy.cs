using MartonioJunior.Collectables.Items;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemEventListener_Dummy: ItemEventListener<int>
    {
        public override int Convert(IItemWallet wallet)
        {
            return wallet?.AmountOf(Item) ?? 0;
        }

        public override void Reset() {}

        public override void Validate() {}
    }
}