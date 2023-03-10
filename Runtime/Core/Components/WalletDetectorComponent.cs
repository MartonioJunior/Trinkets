using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to detect wallets present in a physical space.</summary>
    */
    [AddComponentMenu("Trinkets/Wallet Detector")]
    [DisallowMultipleComponent, SelectionBase]
    public class WalletDetectorComponent: MonoBehaviour
    {
        #region Events
        /**
        <summary>Event triggered when a Wallet enters the trigger area.</summary>
        */
        [Header("Events")]
        [SerializeField] Event<Wallet> OnEnter;
        /**
        <summary>Event triggered when a Wallet leaves the trigger area.</summary>
        */
        [SerializeField] Event<Wallet> OnExit;
        #endregion
        #region MonoBehaviour Lifecycle
        /**
        <summary>OnTriggerEnter is called when a collider enters the trigger.</summary>
        <param name="other">The other Collider involved in this collision.</param>
        */
        void OnTriggerEnter(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnEnter.Invoke(wallet);
            }
        }
        /**
        <summary>Sent when another object enters a trigger collider attached to
        this object (2D physics only).</summary>
        <param name="other">The other Collider2D involved in this collision.</param>
        */
        void OnTriggerEnter2D(Collider2D other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnEnter.Invoke(wallet);
            }
        }
        /**
        <summary>OnTriggerExit is called when a collider has stopped touching the trigger.</summary>
        <param name="other">The other Collider involved in this collision.</param>
        */
        void OnTriggerExit(Collider other)
        {
            if (GetWallet(other.gameObject, out var wallet)) {
                OnExit.Invoke(wallet);
            }
        }
        /**
        <summary>Sent when another object leaves a trigger collider attached
        to this object (2D physics only).</summary>
        <param name="other">The other Collider2D involved in this collision.</param>
        */
        void OnTriggerExit2D(Collider2D other)
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