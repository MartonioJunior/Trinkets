using UnityEditor;
using UnityEngine;
using MartonioJunior.Trinkets;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomPropertyDrawer(typeof(Field<>), true)]
    public class FieldDrawer: PropertyDrawer, IMediaInspector
    {
        #region Variables
        Texture2D background;
        #endregion
        #region PropertyDrawer Implementation
        public override void Build()
        {
            var fieldType = fieldInfo.FieldType.GenericTypeArguments[0];
            var fieldValue = PropertyUtility.GetActualObject<object>(fieldInfo, property);
            string filterLabelText = $"Field Type: {fieldType.Name}";
            string dropMessageText = "Drop Here";
            string valueText = fieldValue.ToString();
            this.LazyLoadMedia();

            var newRect = drawerRect;
            drawerRect.width = drawerRect.height;
            newRect.xMin = drawerRect.width*2;

            EditorGUI.DrawPreviewTexture(drawerRect, background);
            PropertyUtility.DropArea(drawerRect, dropMessageText, FilterToType);
            if (fieldValue == null) {
                valueText = "Empty Field";
            }
            EditorGUI.LabelField(newRect, valueText);
            EditorGUILayout.LabelField(filterLabelText);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2f;
        }
        #endregion
        #region IMediaInspector Implementation
        public bool MediaIsLoaded {get; set;}

        public void LoadMedia()
        {
            const string BoxImagePath = "Gizmos/Field.png";
            background = new Texture2D(1,1).LoadImage(DataPath.PackagePath(BoxImagePath));
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