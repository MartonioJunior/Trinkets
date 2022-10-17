using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    [AddComponentMenu("Trinkets/Wallet Listener")]
    public class WalletListenerComponent: EngineBehaviour, IResourceProcessor<int>
    {
        #region Variables
        [field: SerializeField] public Wallet Wallet {get; set;}
        [field: SerializeField] public Resource Resource {get; set;}
        #endregion
        #region Events
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceScannerComponent.OnAmountChange"/>
        */
        [SerializeField] UnityEvent<int> amountChanged;
        /**
        <remarks>Meant as a event gateway for designers to use in the inspector.
        </remarks>
        <inheritdoc cref="ResourceScannerComponent.OnPresenceUpdate"/>
        */
        [SerializeField] UnityEvent<bool> resourcePresent;
        /**
        <summary>Event invoked when the component updates.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action<int> OnAmountChange;
        /**
        <summary>Event invoked when the component updates.</summary>
        <remarks>Meant as a event gateway for programmers to listen for events.
        </remarks>
        */
        public event Action<bool> OnPresenceUpdate;
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
        #region IResourceProcessor Implementation
        public int Convert(IResourceGroup group)
        {
            return group?.AmountOf(Resource) ?? 0;
        }
        #endregion
        #region Methods
        /**
        <summary>Coroutine responsible for updating the state of the component.
        </summary>
        */
        IEnumerator Start()
        {
            const float UpdateTime = 0.5f;

            while (true) {
                var amount = Convert(Wallet);
                OnAmountChange?.Invoke(amount);
                OnPresenceUpdate?.Invoke(amount > 0);

                yield return new WaitForSeconds(UpdateTime);
            }
        }
        #endregion
    }
}