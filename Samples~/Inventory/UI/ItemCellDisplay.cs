using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.Inventory
{
    public class ItemCellDisplay: UIDisplay
    {
        #region Variables
        [field: SerializeField] public Image Icon {get; private set;}
        [field: SerializeField] public TextMeshProUGUI AmountLabel {get; private set;}
        #endregion
    }
}