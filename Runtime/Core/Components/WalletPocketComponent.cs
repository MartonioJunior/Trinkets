using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to give physical presence to a Wallet.</summary>
    */
    public class WalletPocketComponent: MonoBehaviour
    {
        #region Variables
        /**
        <summary>The wallet stored at the component.</summary>
        */
        [field: SerializeField] public Wallet Wallet {get; set;}
        #endregion
    }
}