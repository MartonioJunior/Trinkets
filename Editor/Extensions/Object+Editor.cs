using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MartonioJunior.Trinkets.Editor {
  public static partial class ObjectExtensions {
    public static bool FilterType(this Object self, Type type, out Object obj) {
      obj = null;

      if (self is GameObject) {
        obj = (self as GameObject).GetComponent(type);
      } else if (self is MonoBehaviour || self is ScriptableObject) {
        if (type.IsAssignableFrom(self.GetType())) {
          obj = self;
        } else {
          return false;
        }
      } else {
        return false;
      }

      return true;
    }
  }
}