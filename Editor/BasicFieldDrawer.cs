using UnityEditor;
using UnityEngine;
using MartonioJunior.Collectables;

namespace MartonioJunior.Collectables.Editor
{
    [CustomPropertyDrawer(typeof(BasicField), true)]
    public class BasicFieldDrawer: PropertyDrawer
    {
        #region PropertyDrawer Implementation
        public override void Build()
        {
            var unityReference = property.FindPropertyRelative("unityObject");
            var objectReference = property.FindPropertyRelative("referenceObject");
            var fieldType = fieldInfo.FieldType.GenericTypeArguments[0];

            var lineHeight = EditorGUIUtility.singleLineHeight;
            var tabRect = new Rect(drawerRect.x, drawerRect.y, drawerRect.width, lineHeight);
            var unityRefRect = new Rect(drawerRect.x, tabRect.y + lineHeight, drawerRect.width, lineHeight);
            PropertyUtility.ObjectField(unityRefRect, property.displayName, ref unityReference, fieldType, true);
            var unityObjRect = new Rect(drawerRect.x, unityRefRect.y + lineHeight, drawerRect.width, lineHeight);

            var unityValue = unityReference.objectReferenceValue;
            if (unityValue == null) {
                if (objectReference == null) {
                    EditorGUI.LabelField(unityObjRect, $"No value yet assigned");
                } else {
                    EditorGUI.PropertyField(unityObjRect, objectReference);
                }
            } else {
                EditorGUI.LabelField(unityObjRect, $"{unityValue}");
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
        #endregion
    }
}