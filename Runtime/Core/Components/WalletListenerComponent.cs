using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Component used to listen for resources inside of a Wallet.</summary>
    */
    [AddComponentMenu("Trinkets/Wallet Listener")]
    public class WalletListenerComponent: MonoBehaviour, IResourceProcessor<int>
    {
        #region Constants
        /**
        <summary>The amount of time (in seconds) between updates.</summary>
        */
        public const float UpdateTime = 0.5f;
        #endregion
        #region Variables
        /**
        <summary>The wallet to be checked by the listener.</summary>
        */
        [field: SerializeField] public Wallet Wallet {get; set;}
        /**
        <summary>The resource to look out for.</summary>
        */
        [field: SerializeField] public Resource Resource {get; set;}
        #endregion
        #region Events
        /**
        <summary>Event invoked when the component updates.</summary>
        */
        [Header("Events")]
        public Event<int> OnAmountChange;
        /**
        <summary>Event invoked when the component updates.</summary>
        */
        public Event<bool> OnPresenceUpdate;
        #endregion
        #region IResourceProcessor Implementation
        /**
        <inheritdoc/>
        */
        public int Convert(IResourceGroup group)
        {
            return group?.AmountOf(Resource) ?? 0;
        }
        #endregion
        #region Methods
        /**
        <summary>Instruction used to determine the timeframe between loops.</summary>
        */
        YieldInstruction YieldUpdate = new WaitForSeconds(UpdateTime);
        /**
        <summary>Coroutine responsible for updating the state of the component.</summary>
        */
        IEnumerator Start()
        {
            while (true) {
                var amount = Convert(Wallet);
                OnAmountChange.Invoke(amount);
                OnPresenceUpdate.Invoke(amount > 0);

                yield return new WaitForSeconds(UpdateTime);
            }
        }
        #endregion
    }
}