using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Interface used to describe a collectable.</summary>
  */
  public interface ICollectable : IResource {
#region Properties
    /**
    <summary>The category of which a collectable belongs to.</summary>
    */
    ICollectableCategory Category { get; }
#endregion
#region Methods
    /**
    <summary>Adds a collectable to a wallet.</summary>
    <param name="destination">The wallet which will receive the
    collectable.</param>
    */
    void Collect(ICollectableWallet destination);
    /**
    <summary>Checks if a collectable is present inside a wallet.</summary>
    <param name="wallet">The wallet to be searched.</param>
    <returns><c>true</c> when the collectable is in the wallet.<br/>
    <c>false</c> when the collectable is not found in the wallet.</returns>
    */
    bool WasCollectedBy(ICollectableWallet wallet);
#endregion
  }
}
