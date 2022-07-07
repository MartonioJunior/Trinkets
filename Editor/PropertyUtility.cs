using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Reference = UnityEngine.Object;

namespace MartonioJunior.Trinkets.Editor
{
    public sealed class PropertyUtility
    {
        public static T GetActualObject<T>(FieldInfo fieldInfo, SerializedProperty property) where T: class
        {
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            if (obj == null) { return null; }
    
            T actualObject = null;
            if (obj.GetType().IsArray) {
                var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
                actualObject = ((T[])obj)[index];
            } else {
                actualObject = obj as T;
            }
            return actualObject;
        }

        private static void LabelField(Rect rect, string text, TextAnchor anchor = TextAnchor.MiddleLeft)
        {
            var selectedStyle = new GUIStyle(GUI.skin.label) { alignment = anchor };
            EditorGUI.LabelField(rect, text, selectedStyle);
        }

        public static string[] GetPropertiesInBaseClass(System.Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var propertiesInBaseClass = new string[fields.Length+1];
            for (int i = 0; i < fields.Length; i++) {
                propertiesInBaseClass[i] = fields[i].Name;
            }
            propertiesInBaseClass[fields.Length] = "m_Script";
            return propertiesInBaseClass;
        }

        public static void InterfaceField(Rect position, string fieldName, ref SerializedProperty property, Type type, bool allowSceneObjects)
        {
            if (!type.IsInterface) return;

            var value = EditorGUI.ObjectField(position, fieldName, property.objectReferenceValue, typeof(Reference), allowSceneObjects);
            if (value?.GetType().IsSubclassOf(type) ?? false) {
                property.objectReferenceValue = value;
            }
        }

        public static void ObjectField(Rect position, string fieldName, ref SerializedProperty property, Type type, bool allowSceneObjects)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(position, fieldName, property.objectReferenceValue, type, allowSceneObjects);
        }

        public static void TabGroup(Rect position, ref int index, params string[] options)
        {
            if (options == null || options.Length == 0) return;

            float width = position.width / options.Length;
            var rect = new Rect(position.x, position.y, width, position.height);
            for (int i = 0; i < options.Length; i++)
            {
                var text = options[i];
                if (i == index) {
                    PropertyUtility.LabelField(rect, text, TextAnchor.MiddleCenter);
                } else if (GUI.Button(rect, text)) {
                    index = i;
                }
                rect.x += width;
            }
        }

        public static void DropArea(Rect dropRect, string message, Action<UnityEngine.Object> receivedDrop)
        {
            Event evt = Event.current;
            GUI.Box(dropRect, message);
        
            switch (evt.type) {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropRect.Contains(evt.mousePosition))
                        return;
                    
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                
                    if (evt.type == EventType.DragPerform) {
                        DragAndDrop.AcceptDrag();
                    
                        foreach (UnityEngine.Object draggedObject in DragAndDrop.objectReferences) {
                            receivedDrop?.Invoke(draggedObject);
                        }
                    }
                    break;
            }
        }
    }
}