using UnityEngine;

namespace MartonioJunior.Collectables.Currency
{
    public interface ICurrency: IResource
    {
        #region Properties
        string Symbol {get;}
        #endregion
    }
}