using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Interface used to obtain data from a resource wallet.</summary>
  */
  public interface IResourceProcessor<TInput, TOutput>
      where TInput : IWallet {
    /**
    <summary>Transform wallet information into data.</summary>
    <param name="wallet">The wallet used as the source of data.</param>
    <returns>The data acquired from the operation.</returns>
    */
    TOutput Convert(TInput wallet);
  }
}
