using MartonioJunior.Collectables.Items;

namespace Tests.MartonioJunior.Collectables.Items
{
    public class ItemScanner_Dummy: ItemScanner
    {
        public override bool FulfillsCriteria(IItemWallet wallet)
        {
            return wallet != null;
        }

        public override bool PerformTax(IItemWallet wallet)
        {
            if (wallet == null) return false;

            wallet.Clear();
            return true;
        }
    }
}