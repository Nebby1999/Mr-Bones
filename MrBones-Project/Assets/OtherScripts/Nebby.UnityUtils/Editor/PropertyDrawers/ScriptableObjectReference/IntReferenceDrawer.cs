using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using Nebby.UnityUtils.Editor.Utils;
using Nebby.UnityUtils.Editor.Common;

namespace Nebby.UnityUtils.Editor.PropertyDrawers
{

    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer
    {
        public bool usingConstant;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            usingConstant = GetConstantValue(property);
            EditorGUI.BeginProperty(position, label, property);
            
            //Field itself
            var fieldRect = new Rect(position.x, position.y, position.width - 10, position.height);
            EditorGUI.PropertyField(fieldRect,
                usingConstant ? property.FindPropertyRelative("constant") : property.FindPropertyRelative("assetReference"),
                new GUIContent(property.displayName));

            //Context menu area and menu
            var contextRect = new Rect(fieldRect.xMax, position.y, 10, position.height);
            EditorGUI.DrawTextureTransparent(contextRect, AssetLocator.ContextMenuIndicator, ScaleMode.ScaleToFit);
            if(Event.current.type == EventType.ContextClick)
            {
                Vector2 mousePos = Event.current.mousePosition;
                if(contextRect.Contains(mousePos))
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent($"Use Constant"), usingConstant, () =>
                    {
                        SetConstantValue(property, !usingConstant);
                    });
                    menu.ShowAsContext();
                    Event.current.Use();
                }
            }
            EditorGUI.EndProperty();
        }

        bool GetConstantValue(SerializedProperty prop)
        {
            return prop.FindPropertyRelative(nameof(IntReference.useConstant)).boolValue;
        }

        void SetConstantValue(SerializedProperty prop, bool booleanValue)
        {
            prop.FindPropertyRelative(nameof(IntReference.useConstant)).boolValue = booleanValue;
            prop.serializedObject.ApplyModifiedProperties();
        }
    }
}