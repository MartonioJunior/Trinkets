using UnityEngine;

namespace MartonioJunior.Trinkets.Currency
{
    public interface ICurrency: IResource
    {
        #region Properties
        string Symbol {get;}
        #endregion
    }
}