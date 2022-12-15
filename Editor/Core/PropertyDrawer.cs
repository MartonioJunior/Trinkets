using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace MartonioJunior.Trinkets.Editor
{
    public abstract class PropertyDrawer: UnityEditor.PropertyDrawer
    {
        #region Constants
        public const string DrawersPath = "Packages/com.martoniojunior.trinkets/Editor/Drawers/";
        #endregion
        #region Abstract
        public abstract void Bind(VisualElement root, SerializedProperty property);
        public abstract string VisualTreePath();
        #endregion
        #region UnityEditor.PropertyDrawer
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var assetPath = DrawersPath+VisualTreePath()+".uxml";
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(assetPath);

            if (uxml == null) return base.CreatePropertyGUI(property);
                
            var root = new VisualElement();
            uxml.CloneTree(root);
            Bind(root, property);
            return root;
        }
        #endregion
    }
}