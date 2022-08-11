using UnityEngine;

namespace MartonioJunior.Trinkets.Editor {
  public static class Style {
#region Static Properties
    public static GUIStyle New => new GUIStyle();
#endregion
#region Static Methods
    public static GUIStyle BasedOn(GUIStyle reference) {
      return new GUIStyle(reference);
    }
#endregion
  }
}