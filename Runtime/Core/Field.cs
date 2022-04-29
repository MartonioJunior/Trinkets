using System;
using UnityEngine;
using Reference = UnityEngine.Object;

namespace MartonioJunior.Collectables
{
    public class BasicField {}
    [Serializable]
    public class Field<IType>: BasicField
    {
        #region Variables
        [SerializeField] Reference unityObject;
        [SerializeField] object referenceObject;
        #endregion
        #region Constructors
        public Field()
        {
            unityObject = null;
            referenceObject = null;
        }

        public Field(IType obj)
        {
            if (obj is Reference) unityObject = obj as Reference;
            else referenceObject = obj;
        }
        #endregion
        #region Methods
        public void Clear()
        {
            unityObject = null;
            referenceObject = null;
        }

        public bool HasValue()
        {
            return unityObject != null || referenceObject != null;
        }

        public void Set(Reference obj)
        {
            if (obj is IType) {
                unityObject = obj;
                referenceObject = null;
            }
        }

        public void Set(object obj)
        {
            if (obj is IType) {
                referenceObject = obj;
                unityObject = null;
            }
        }

        public override string ToString()
        {
            if (unityObject != null) return unityObject.ToString();
            else if (referenceObject != null) return referenceObject.ToString();
            else return "Empty Field";
        }

        public IType Unpack()
        {
            if (unityObject != null) return unityObject.UnsafeCast<IType>();
            else if (referenceObject != null) return referenceObject.UnsafeCast<IType>();
            else return default(IType);
        }
        #endregion
        #region Operators
        public static implicit operator IType(Field<IType> field) => field.Unpack();
        #endregion
    }
}