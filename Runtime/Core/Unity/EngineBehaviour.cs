using UnityEngine;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Abstract MonoBehaviour which requires implementation for what
  happens at the start and end of the lifecycle.</summary>
  */
  public abstract class EngineBehaviour : MonoBehaviour {
#region Abstract Methods
    /**
    <summary>Called when the component is awoken.</summary>
    <remarks>Ideal for linking events and defining base values.</remarks>
    */
    public abstract void Setup();
    /**
    <summary>Called when the component is destroyed.</summary>
    <remarks>Ideal for disposing of objects and unlinking events.</remarks>
    */
    public abstract void TearDown();
#endregion
#region Methods
    /**
    <inheritdoc />
    */
    private void Awake() { Setup(); }
    /**
    <inheritdoc />
    */
    private void OnDestroy() { TearDown(); }
#endregion
  }
}