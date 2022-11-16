using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace Nebby.Editor
{
    public abstract class IMGUIInspector<TObject> : UnityEditor.Editor where TObject : UnityEngine.Object
    {
        protected TObject TargetType { get => target as TObject; }

        public sealed override void OnInspectorGUI()
        {
            DrawGUI();
            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void DrawGUI();

        protected SerializedProperty GetProp(string propName) => serializedObject.FindProperty(propName);
        protected SerializedProperty GetProp(SerializedProperty parentProperty, string propName) => parentProperty.FindPropertyRelative(propName);
        protected void DrawProp(SerializedProperty prop) => EditorGUILayout.PropertyField(prop, true);
        protected void DrawProp(string propName) => EditorGUILayout.PropertyField(serializedObject.FindProperty(propName), true);
        protected void DrawProp(SerializedProperty parentProperty, string propName) => EditorGUILayout.PropertyField(parentProperty.FindPropertyRelative(propName), true);
    } 
}