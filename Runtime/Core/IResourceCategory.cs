using UnityEngine;

namespace MartonioJunior.Trinkets
{
/**
<summary>Interface used for describing a category of
in-game resources.</summary>
*/
public interface IResourceCategory
{
    /**
    <summary>The name of the category</summary>
    */
    string Name {
        get;
    }
}
}