using MartonioJunior.Trinkets.Currencies;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MartonioJunior.Trinkets.Editor
{
    [CustomPropertyDrawer(typeof(ResourceData))]
    public class ResourceDataDrawer: PropertyDrawer
    {
        #region Variables
        #endregion
        #region PropertyDrawer Implementation
        public override void Bind(VisualElement root, SerializedProperty property)
        {
            BindResourceProperty(root);
            BindAmountProperty(root);
        }

        public override string VisualTreePath()
        {
            return "ResourceData";
        }
        #endregion
        #region Methods
        private void BindResourceProperty(VisualElement root)
        {
            var resourceField = root.Q<ObjectField>("resource");
            resourceField.objectType = typeof(Resource);
            resourceField.bindingPath = "_resource";
        }

        private void BindAmountProperty(VisualElement root)
        {
            var amountField = root.Q<IntegerField>("amount");
            amountField.bindingPath = "amount";
        }
        #endregion
    }
}