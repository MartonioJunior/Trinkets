using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MartonioJunior.Trinkets {
  /**
  <summary>Class used to encapsulate interface fields</summary>
  */
  [Serializable]
  public class Field<T> {
#region Variables
    /**
    <summary>Container for objects which inherit from
    <c>UnityEngine.Object</c>.</summary>
    */
    [SerializeField]
    Object unityObject;
    /**
    <summary>Container for C# structs and classes which do not inherit from
    <c>UnityEngine.Object</c>.</summary>
    */
    [SerializeReference]
    object csharpObject;
#endregion
#region Constructors
    /**
    <summary>Initializes an empty field.</summary>
    */
    public Field() {
      csharpObject = null;
      unityObject = null;
    }
    /**
    <summary>Initializes the field with a starting value.</summary>
    */
    public Field(T obj) { Set(obj); }
#endregion
#region Methods
    /**
    <summary>Removes the value stored on the field.</summary>
    */
    public void Clear() {
      csharpObject = null;
      unityObject = null;
    }
    /**
    <summary>Returns the value stored inside the field.</summary>
    <param name="value">The specified value.</param>
    <returns><c>true</c> when the field contains a value.<br/>
    <c>false</c> when the field is empty.</returns>
    */
    public bool Get(out T value) {
      if (unityObject is T validObject) {
        value = validObject;
        return true;
      } else if (csharpObject is T obj) {
        value = obj;
        return true;
      } else {
        value = default(T);
        return false;
      }
    }
    /**
    <summary>Does the field have a value stored?</summary>
    <returns><c>true</c> when the field has a value.<br/>
    <c>false</c> when the field is empty.</returns>
    */
    public bool HasValue() {
      return unityObject != null || csharpObject != null;
    }
    /**
    <summary>Sets a new value for the field.</summary>
    <param name="obj">The new value to be stored.</param>
    */
    public void Set(T obj) {
      if (obj == null) {
        Clear();
      } else if (obj is Object unityObj) {
        unityObject = unityObj;
        csharpObject = null;
      } else {
        csharpObject = obj;
        unityObject = null;
      }
    }
    /**
    <summary>Returns a visual description of the field.</summary>
    <returns>The string representation of the stored value."</returns>
    <remarks>An empty field returns "Empty Field" as a message</remarks>
    */
    public override string ToString() {
      if (unityObject != null)
        return unityObject.ToString();
      else if (csharpObject != null)
        return csharpObject.ToString();
      else
        return "Empty Field";
    }
    /**
    <summary>Returns the value stored on the field</summary>
    <returns>The field's value.</returns>
    */
    public T Unwrap() {
      Get(out var value);
      return value;
    }
#endregion
#region Operators
    public static implicit operator T(Field<T> field) => field.Unwrap();
    public static implicit operator Field<T>(T obj) => new Field<T>(obj);
#endregion
  }
}
