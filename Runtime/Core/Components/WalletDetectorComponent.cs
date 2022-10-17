using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to detect wallets present in a physical space.</summary>
    */
    public class WalletDetectorComponent: EngineBehaviour
    {
        #region Events
        [SerializeField] UnityEvent<Wallet> OnEnter;
        [SerializeField] UnityEvent<Wallet> OnExit;
        #endregion
        #region MonoBehaviour Lifecycle
        void OnTriggerEnter(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnEnter?.Invoke(wallet);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnExit?.Invoke(wallet);
            }
        }
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
        #region Methods
        private bool GetWallet(GameObject gameObject, out Wallet wallet)
        {
            var pocket = gameObject.GetComponentInChildren<WalletPocketComponent>();
            if (pocket != null) {
                wallet = pocket.Wallet;
                return true;
            } else {
                wallet = default;
                return false;
            }
        }
        #endregion
    }
}