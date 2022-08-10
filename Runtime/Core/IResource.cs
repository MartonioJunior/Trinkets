using UnityEngine;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used for the implementation of in-game resources.</summary>
    */
    public interface IResource: IRepresentable
    {
        /**
        <summary>The value attributed to a resource.</summary>
        */
        int Value {get;}
    }
}