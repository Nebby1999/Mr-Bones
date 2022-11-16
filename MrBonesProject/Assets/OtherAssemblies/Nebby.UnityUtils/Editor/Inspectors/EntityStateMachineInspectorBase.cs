using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Nebby.Editor.Inspectors
{
    public abstract class EntityStateMachineInspectorBase<T> : IMGUIInspector<T> where T : EntityStateMachineBase
    {
        private bool foldout;
        private Vector2 scrollPos;
        private string SearchBar
        {
            get => _searchBar;
            set
            {
                if(_searchBar != value)
                {
                    _searchBar = value;
                    FilterInstanceFields();
                }
            }
        }
        private string _searchBar;
        private Type CurrentInspectedStateType
        {
            get => _currentInspectedStateType;

            set
            {
                if(_currentInspectedStateType != value)
                {
                    _currentInspectedStateType = value;
                    GetInstanceFields();
                    FilterInstanceFields();
                }
            }
        }
        private Type _currentInspectedStateType;
        private FieldInfo[] _inspectedTypeInstanceFields = Array.Empty<FieldInfo>();
        private FieldInfo[] _filteredTypeInstanceFields = Array.Empty<FieldInfo>();

        protected override void DrawGUI()
        {
            CurrentInspectedStateType = TargetType.CurrentState?.GetType();
            string text = CurrentInspectedStateType?.AssemblyQualifiedName ?? "Not in Playmode or Null";

            EditorGUILayout.BeginVertical();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(new GUIContent($"Current State:"), text);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.BeginVertical("box");
            foldout = EditorGUILayout.Foldout(foldout, "Instance Fields", true);
            if(foldout)
            {
                EditorGUI.indentLevel++;
                DrawInspectedInstanceFields();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUI.EndDisabledGroup();

            DrawPropertiesExcluding(serializedObject, "m_Script");
            
            EditorGUILayout.EndVertical();
        }

        private void DrawInspectedInstanceFields()
        {
            if(CurrentInspectedStateType == null)
            {
                EditorGUILayout.HelpBox("Current state is null", MessageType.Warning);
                return;
            }


            List<FieldInfo> undrawableFields = new List<FieldInfo>();
            SearchBar = EditorGUILayout.DelayedTextField(new GUIContent("Search"), SearchBar);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true, GUILayout.Width(Screen.width), GUILayout.Height(100));
            EditorGUI.BeginDisabledGroup(true);
            foreach(FieldInfo info in _filteredTypeInstanceFields)
            {
                if(!IMGUIUtil.TryDrawFieldFromFieldInfo(info, TargetType.CurrentState))
                {
                    undrawableFields.Add(info);
                }
            }
            EditorGUI.EndDisabledGroup();

            if(undrawableFields.Count > 0)
            {
                DrawUndrawableFields(undrawableFields);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawUndrawableFields(IEnumerable<FieldInfo> fields)
        {
            var text = fields.Select(x => $"{x.Name}, {x.FieldType}").ToArray();
            EditorGUILayout.HelpBox($"The following {text.Length} could not be drawn:\n {string.Join("\n", text)}", MessageType.Warning);
        }
        private void GetInstanceFields()
        {
            if (CurrentInspectedStateType == null)
                return;

            List<FieldInfo> instanceFields = new List<FieldInfo>();
            var currentType = CurrentInspectedStateType;
            while(currentType.BaseType != null)
            {
                instanceFields.AddRange(currentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly));
                currentType = currentType.BaseType;
            }

            _inspectedTypeInstanceFields = instanceFields.OrderBy(x => x.FieldType.Name).ToArray();
        }

        private void FilterInstanceFields()
        {
            if(SearchBar.IsNullOrEmptyOrWhitespace())
            {
                _filteredTypeInstanceFields = _inspectedTypeInstanceFields;
                return;
            }

            _filteredTypeInstanceFields = _inspectedTypeInstanceFields.Where(f => f.Name.Contains(SearchBar, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
    }
}
