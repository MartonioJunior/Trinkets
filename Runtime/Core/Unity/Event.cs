using System;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.Trinkets
{
    /**
    <summary>Structure used to combine <c>Action</c> and <c>UnityEvent</c> in a single structure</summary>
    */
    [Serializable]
    public struct Event
    {
        #region Variables
        /**
        <summary>Event for registering actions via inspector.</summary>
        */
        [SerializeField] UnityEvent unityEvent;
        /**
        <summary>Event for registering actions via code.</summary>
        */
        [SerializeField] Action action;
        #endregion
        #region Methods
        /**
        <summary>Fires the event.</summary>
        */
        public void Invoke()
        {
            unityEvent?.Invoke();
            action?.Invoke();
        }
        #endregion
        #region Operators
        /**
        <summary>Registers an action to the <c>Event</c> object.</summary>
        */
        public static Event operator +(Event lhs, Action rhs)
        {
            lhs.action += rhs;
            return lhs;
        }
        /**
        <summary>Removes an action from the <c>Event</c> object.</summary>
        */
        public static Event operator -(Event lhs, Action rhs)
        {
            lhs.action -= rhs;
            return lhs;
        }
        #endregion
    }

    /**
    <summary>Structure used to combine <c>Action<T></c> and <c>UnityEvent<T></c> in a single structure</summary>
    */
    [Serializable]
    public struct Event<T>
    {
        #region Variables
        /**
        <inheritdoc cref="Event.unityEvent" />
        */
        [SerializeField] UnityEvent<T> unityEvent;
        /**
        <inheritdoc cref="Event.action" />
        */
        [SerializeField] Action<T> action;
        #endregion
        #region Methods
        /**
        <param name="value">The argument to be passed by the event.</param>
        <inheritdoc cref="Event.Invoke" />
        */
        public void Invoke(T value)
        {
            unityEvent?.Invoke(value);
            action?.Invoke(value);
        }
        #endregion
        #region Operators
        /**
        <inheritdoc cref="Event.operator+"/>
        */
        public static Event<T> operator +(Event<T> lhs, Action<T> rhs)
        {
            lhs.action += rhs;
            return lhs;
        }
        /**
        <inheritdoc cref="Event.operator-"/>
        */
        public static Event<T> operator -(Event<T> lhs, Action<T> rhs)
        {
            lhs.action -= rhs;
            return lhs;
        }
        #endregion
    }
}