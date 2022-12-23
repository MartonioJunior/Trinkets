using MartonioJunior.Trinkets;
using UnityEngine;

namespace Sample.Inventory
{
    public static partial class ItemCellDisplayExtensions
    {
        public static void Set(this ItemCellDisplay display, IResourceData data)
        {
            display.Icon.sprite = data.Resource.Image;
            display.AmountLabel.text = data.Amount.ToString();
        }
    }
}