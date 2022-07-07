using MartonioJunior.Trinkets.Items;

namespace Tests.MartonioJunior.Trinkets.Items
{
    public class ItemEventListener_Dummy: ItemEventListener<int>
    {
        public override int[] Convert(IItem[] items)
        {
            return new int[1]{items?.Length ?? 0};
        }
    }
}