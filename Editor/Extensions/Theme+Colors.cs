using UnityEngine;

namespace MartonioJunior.Trinkets.Editor {
  public static class Theme {
#region Subclasses
    public static class H1 {
#region Variables
      public static Color BGColor {
        get {
          ColorUtility.TryParseHtmlString("#3BB273", out Color color);
          return color;
        }
      }
      public static Color TextColor {
        get {
          ColorUtility.TryParseHtmlString("#282828", out Color color);
          return color;
        }
      }
#endregion
    }

    public static class H2 {
#region Variables
      public static Color BGColor {
        get {
          ColorUtility.TryParseHtmlString("#282828", out Color color);
          return color;
        }
      }

      public static Color TextColor {
        get {
          ColorUtility.TryParseHtmlString("#D2D2D2", out Color color);
          return color;
        }
      }
#endregion
    }
#endregion
  }
}
