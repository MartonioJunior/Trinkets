using UnityEngine;

namespace MartonioJunior.Trinkets.Editor
{
    public static partial class GUIStyleExtensions
    {
        #region Static Methods
        public static GUIStyle BG(this GUIStyle style, Color color)
        {
            var px = new Color[1]{color};

            var result = new Texture2D(1,1);
            result.SetPixels(px);
            result.Apply();

            style.normal.background = result;
            return style;
        }
        #endregion
    }
}