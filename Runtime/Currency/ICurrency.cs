using UnityEngine;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Interface which describes a in-game currency.</summary>
    */
    public interface ICurrency: IResource
    {
        #region Properties
        /**
        <summary>An abbreviated representation of the currency.</summary>
        */
        string Symbol {get;}
        #endregion
    }
}