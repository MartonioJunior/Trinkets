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
        /**
        <summary>Should the amount of a resource be counted?</summary>
        <remarks>Return <c>true</c> when the resource can be quantified in numbers.
        <br/>Return <c>false</c> when the resource should not be counted.</remarks>
        */
        bool Quantifiable {get;}
    }
}