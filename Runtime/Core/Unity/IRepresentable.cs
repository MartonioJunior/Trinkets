using UnityEngine;

namespace MartonioJunior.Trinkets
{
    public interface IRepresentable
    {
        /**
        <summary>A textual representation for the object.</summary>
        */
        string Name {get;}
        /**
        <summary>A visual representation for the object.</summary>
        */
        Sprite Image {get;}
    }
}