using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor
{
    public abstract class IMGUIEditorWindow : ExtendedEditorWindow
    {
        private delegate object FieldDrawHandler(FieldInfo fieldInfo, object value);
        private static readonly Dictionary<Type, FieldDrawHandler> typeDrawers = new Dictionary<Type, FieldDrawHandler>
        {
            [typeof(bool)] = (fieldInfo, value) => EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(fieldInfo.Name), (bool)value),
            [typeof(long)] = (fieldInfo, value) => EditorGUILayout.LongField(ObjectNames.NicifyVariableName(fieldInfo.Name), (long)value),
            [typeof(int)] = (fieldInfo, value) => EditorGUILayout.IntField(ObjectNames.NicifyVariableName(fieldInfo.Name), (int)value),
            [typeof(float)] = (fieldInfo, value) => EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(fieldInfo.Name), (float)value),
            [typeof(double)] = (fieldInfo, value) => EditorGUILayout.DoubleField(ObjectNames.NicifyVariableName(fieldInfo.Name), (double)value),
            [typeof(string)] = (fieldInfo, value) => EditorGUILayout.TextField(ObjectNames.NicifyVariableName(fieldInfo.Name), (string)value),
            [typeof(Vector2)] = (fieldInfo, value) => EditorGUILayout.Vector2Field(ObjectNames.NicifyVariableName(fieldInfo.Name), (Vector2)value),
            [typeof(Vector3)] = (fieldInfo, value) => EditorGUILayout.Vector3Field(ObjectNames.NicifyVariableName(fieldInfo.Name), (Vector3)value),
            [typeof(Color)] = (fieldInfo, value) => EditorGUILayout.ColorField(ObjectNames.NicifyVariableName(fieldInfo.Name), (Color)value),
            [typeof(Color32)] = (fieldInfo, value) => (Color32)EditorGUILayout.ColorField(ObjectNames.NicifyVariableName(fieldInfo.Name), (Color32)value),
            [typeof(AnimationCurve)] = (fieldInfo, value) => EditorGUILayout.CurveField(ObjectNames.NicifyVariableName(fieldInfo.Name), (AnimationCurve)value ?? new AnimationCurve()),
        };


        protected abstract void OnGUI();

        protected SerializedProperty GetProp(string propName, SerializedObject so = null) => so == null ? SerializedObject.FindProperty(propName) : so.FindProperty(propName);
        protected SerializedProperty GetProp(SerializedProperty parentProperty, string propName) => parentProperty.FindPropertyRelative(propName);
        protected void DrawProp(SerializedProperty prop) => EditorGUILayout.PropertyField(prop, true);
        protected void DrawProp(string propName, SerializedObject so = null) => EditorGUILayout.PropertyField(so == null ? SerializedObject.FindProperty(propName) : so.FindProperty(propName), true);
        protected void DrawProp(SerializedProperty parentProperty, string propName) => EditorGUILayout.PropertyField(parentProperty.FindPropertyRelative(propName), true);

        protected bool DrawFieldFromFieldInfo(FieldInfo fieldInfo, object objectInstance)
        {
            try
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(fieldInfo.FieldType))
                {
                    var objectValue = (UnityEngine.Object)fieldInfo.GetValue(objectInstance);
                    EditorGUILayout.ObjectField(new GUIContent(ObjectNames.NicifyVariableName(fieldInfo.Name)), objectValue, fieldInfo.FieldType, false);
                    return true;
                }

                if (typeDrawers.TryGetValue(fieldInfo.FieldType, out var drawer))
                {
                    drawer(fieldInfo, fieldInfo.GetValue(objectInstance));
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }
}