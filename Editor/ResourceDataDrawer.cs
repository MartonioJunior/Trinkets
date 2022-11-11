using MartonioJunior.Trinkets.Currencies;
using UnityEditor;
using UnityEngine;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomPropertyDrawer(typeof(ResourceData))]
    public class ResourceDataDrawer: PropertyDrawer
    {
        #region Variables
        Resource resource;
        #endregion
        #region PropertyDrawer Implementation
        public override void Build()
        {
            var serializedAmount = property.FindPropertyRelative("amount");
            var objectResourceProperty = property.FindPropertyRelative("_resource");

            float halfHeight = drawerRect.height / 2;
            var rect = new Rect(drawerRect.x, drawerRect.y, drawerRect.width, halfHeight);
            EditorGUI.PropertyField(rect, objectResourceProperty);
            rect.y += halfHeight;
            EditorGUI.PropertyField(rect, serializedAmount);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2;
        }
        #endregion
    }
}