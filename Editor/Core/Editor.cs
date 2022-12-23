using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace MartonioJunior.Trinkets.Editor
{
    public abstract class Editor: UnityEditor.Editor
    {
        #region Variables
        [SerializeField] protected VisualTreeAsset uxml;
        #endregion
        #region Abstract
        public abstract void Bind(VisualElement root);
        #endregion
        #region UnityEditor.Editor Implementation
        public override VisualElement CreateInspectorGUI()
        {
            if (uxml == null) return base.CreateInspectorGUI();
                
            var root = new VisualElement();
            uxml.CloneTree(root);
            Bind(root);
            return root;
        }
        #endregion
        #region Methods
        protected VisualElement Default()
        {
            var container = new VisualElement();
            DefaultBind(container);
            return container;
        }

        protected void DefaultBind(VisualElement container)
        {
            InspectorElement.FillDefaultInspector(container, serializedObject, this); 
        }
        #endregion
    }
}