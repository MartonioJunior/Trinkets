using System;
using UnityEngine;

namespace MartonioJunior.Trinkets.Editor {
  public static class Present {
#region Classes
    public class HorizontalLayout : IDisposable {
#region Constructors
      public HorizontalLayout(params GUILayoutOption[] options) {
        GUILayout.BeginHorizontal(options);
      }

      public HorizontalLayout(GUIStyle style,
                              params GUILayoutOption[] options) {
        GUILayout.BeginHorizontal(style, options);
      }
#endregion
#region IDisposable Implementation
      public void Dispose() { GUILayout.EndHorizontal(); }
#endregion
    }

    public class VerticalLayout : IDisposable {
#region Constructors
      public VerticalLayout(params GUILayoutOption[] options) {
        GUILayout.BeginVertical(options);
      }

      public VerticalLayout(GUIStyle style, params GUILayoutOption[] options) {
        GUILayout.BeginVertical(style, options);
      }
#endregion
#region IDisposable Implementation
      public void Dispose() { GUILayout.EndVertical(); }
#endregion
    }
#endregion
#region Static Methods
    public static HorizontalLayout
    Horizontal(params GUILayoutOption[] options) {
      return new HorizontalLayout(options);
    }

    public static HorizontalLayout
    Horizontal(GUIStyle style, params GUILayoutOption[] options) {
      return new HorizontalLayout(style, options);
    }

    public static void FlexibleLabel(string text,
                                     params GUILayoutOption[] options) {
      GUILayout.FlexibleSpace();
      GUILayout.Label(text, options);
      GUILayout.FlexibleSpace();
    }

    public static VerticalLayout Vertical(params GUILayoutOption[] options) {
      return new VerticalLayout(options);
    }

    public static VerticalLayout Vertical(GUIStyle style,
                                          params GUILayoutOption[] options) {
      return new VerticalLayout(style, options);
    }
#endregion
  }
}