using UnityEngine;

namespace MartonioJunior.Trinkets.Currencies
{
    /**
    <summary>Interface which describes the basic currency operations.</summary>
    */
    public interface ICurrencyOperator
    {
        #region Methods
        /**
        <summary>Adjusts the amount of a currency.</summary>
        <param name="currency">The currency to be adjusted.</param>
        <param name="delta">The amount to be changed.<br/>
        Positive values increase the amount.<br/>
        Negative values take away the amount.</param>
        */
        void Change(ICurrency currency, int delta);
        /**
        <summary>Reset the amount of a currency back to zero.</summary>
        <param name="currency">The currency to be reset.</param>
        */
	    void Reset(ICurrency currency);
        #endregion
    }
}