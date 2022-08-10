using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to describe an object capable of inserting
  resources into a wallet.</summary>
  */
  public interface IResourceInstancer<T>
      where T : IWallet {
    /**
    <summary>Inserts resources into a wallet.</summary>
    <param name="wallet">The receiving wallet.</param>
    */
    void AddTo(T wallet);
  }
}