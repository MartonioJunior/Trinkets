using UnityEditor;
using UnityEngine;
using MartonioJunior.Collectables;

namespace MartonioJunior.Collectables.Editor
{
    [CustomPropertyDrawer(typeof(Field<>), true)]
    public class FieldDrawer: PropertyDrawer
    {
        #region PropertyDrawer Implementation
        public override void Build()
        {
            var fieldType = fieldInfo.FieldType.GenericTypeArguments[0];
            string message = $"Drag and Drop {fieldType.Name} here";

            PropertyUtility.DropArea(drawerRect, message, FilterToType);
            EditorGUILayout.LabelField(PropertyUtility.GetActualObject<object>(fieldInfo, property).ToString());
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2f;
        }
        #endregion
        #region Methods
        private void FilterToType(UnityEngine.Object obj)
        {
            var unityReference = property.FindPropertyRelative("unityObject");
            var fieldType = fieldInfo.FieldType.GenericTypeArguments[0];

            if (obj.FilterType(fieldType, out Object temp)) {
                unityReference.objectReferenceValue = temp;
            }
        }
        #endregion
    }
}