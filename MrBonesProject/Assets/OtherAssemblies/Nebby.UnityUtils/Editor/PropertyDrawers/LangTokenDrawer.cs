using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(LangTokenAttribute))]
    public class LangTokenDrawer : IMGUIPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);


            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox(position, $"Property {property.name} is not a string property.", MessageType.Error);
            }
            else
            {
                string input = EditorGUI.TextField(position, property.displayName, property.stringValue);
                property.stringValue = input.ToUpperInvariant();
            }

            EditorGUI.EndProperty();
        }
    }
}
