using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ChildLocator.NameTransformPair))]
    public class NameTransformPairDrawer : PropertyDrawer
    {
        public SerializedProperty name;
        public SerializedProperty tiedTransform;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            name = property.FindPropertyRelative("name");
            tiedTransform = property.FindPropertyRelative("tiedTransform");
            EditorGUI.BeginProperty(position, label, property);

            Rect nameRect = new Rect(position.x, position.y, position.width * 0.75f, position.height);
            EditorGUI.PropertyField(nameRect, name, label);

            GUIContent empty = new GUIContent();
            Rect tiedTransformRect = new Rect(nameRect.xMax, position.y, position.width * 0.25f, position.height);
            EditorGUI.PropertyField(tiedTransformRect, tiedTransform, empty);

            EditorGUI.EndProperty();
        }
    }
}
