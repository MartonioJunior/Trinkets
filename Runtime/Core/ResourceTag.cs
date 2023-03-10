using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Object used to categorize resources in a game.</summary>
    */
    public class ResourceTag: ScriptableObject, IResourceTag
    {
        #region IResource Implementation
        /**
        <inheritdoc />
        */
        [field: SerializeField] public string Name {get; set;}
        /**
        <inheritdoc />
        */
        [field: SerializeField] public Sprite Image {get; set;}
        #endregion
    }
}