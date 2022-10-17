using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to give physical presence to a Wallet.</summary>
    */
    public class WalletPocketComponent: EngineBehaviour
    {
        #region Variables
        [field: SerializeField] public Wallet Wallet {get; private set;}
        #endregion
        #region EngineBehaviour Implementation
        /**
        <inheritdoc />
        */
        public override void Setup() {}
        /**
        <inheritdoc />
        */
        public override void TearDown() {}
        #endregion
    }
}