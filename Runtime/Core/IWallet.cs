using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to describe a wallet of resources.</summary>
  <remarks>To add management capabilities, use it together with
  the <c>IResourceManager</c> interface.</remarks>
  */
  public interface IWallet {
    /**
    <summary>Returns a wallet to it's empty state.</summary>
    */
    void Clear();
  }
}
