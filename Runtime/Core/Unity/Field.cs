using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MartonioJunior.Collectables
{
    [Serializable]
    public class Field<T>
    {
        #region Variables
        [SerializeField] Object unityObject;
        [SerializeReference] object csharpObject;
        #endregion
        #region Constructors
        public Field()
        {
            csharpObject = null;
            unityObject = null;
        }

        public Field(T obj)
        {
            Set(obj);
        }
        #endregion
        #region Methods
        public void Clear()
        {
            csharpObject = null;
            unityObject = null;
        }

        public bool Get(out T value)
        {
            if (unityObject != null) {
                value = unityObject.UnsafeCast<T>();
                return true;
            } else if (csharpObject != null) {
                value = csharpObject.UnsafeCast<T>();
                return true;
            } else {
                value = default(T);
                return false;
            }
        }

        public bool HasValue()
        {
            return unityObject != null || csharpObject != null;
        }

        public void Set(T obj)
        {
            if (obj == null) {
                Clear();
            } else if (obj is Object) {
                unityObject = obj.UnsafeCast<Object>();
                csharpObject = null;
            } else {
                csharpObject = obj;
                unityObject = null;
            }
        }

        public override string ToString()
        {
            if (unityObject != null) return unityObject.ToString();
            else if (csharpObject != null) return csharpObject.ToString();
            else return "Empty Field";
        }

        public T Unwrap()
        {
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