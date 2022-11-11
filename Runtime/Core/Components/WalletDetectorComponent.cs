using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to detect wallets present in a physical space.</summary>
    */
    public class WalletDetectorComponent: MonoBehaviour
    {
        #region Events
        /**
        <summary>Event triggered when a Wallet enters the trigger area.</summary>
        */
        [SerializeField] Event<Wallet> OnEnter;
        /**
        <summary>Event triggered when a Wallet leaves the trigger area.</summary>
        */
        [SerializeField] Event<Wallet> OnExit;
        #endregion
        #region MonoBehaviour Lifecycle
        /**
        <summary>Function called when a object enters the trigger area.</summary>
        */
        void OnTriggerEnter(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnEnter.Invoke(wallet);
            }
        }
        /**
        <summary>Function called when a object exits the trigger area.</summary>
        */
        void OnTriggerExit(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnExit.Invoke(wallet);
            }
        }
        #endregion
        #region Methods
        /**
        <summary>Localizes a wallet inside a GameObject hierarchy.</summary>
        <param name="gameObject">The root of the search tree.</param>
        <param name="wallet">The wallet found by the search.</param>
        <returns><c>true</c> when the search is successful.
        <c>false</c> when nothing is found.</returns>
        */
        public bool GetWallet(GameObject gameObject, out Wallet wallet)
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