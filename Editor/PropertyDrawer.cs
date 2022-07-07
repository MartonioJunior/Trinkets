using UnityEngine;
using UnityEditor;

namespace MartonioJunior.Trinkets.Editor
{
    public abstract class PropertyDrawer: UnityEditor.PropertyDrawer
    {
        #region Variables
        protected Rect drawerRect;
        protected SerializedProperty property;
        protected GUIContent label;
        #endregion
        #region Abstract
        public abstract void Build();
        #endregion
        #region Methods
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            this.drawerRect = position;
            this.property = property;
            this.label = label;
            Build();
        }
        #endregion
    }
}