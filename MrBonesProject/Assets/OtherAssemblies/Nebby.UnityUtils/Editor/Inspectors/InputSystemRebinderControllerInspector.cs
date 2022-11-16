using Nebby.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Editor;

namespace Nebby.Editor.Inspectors
{
    [CustomEditor(typeof(InputSystemRebinderController))]
    public class InputSystemRebinderControllerInspector : IMGUIInspector<InputSystemRebinderController>
    {
        private InputActionAsset actionAsset;
        private SerializedProperty targetAsset;

        private string[] actionMaps = Array.Empty<string>();
        private int selectedActionMap = -1;
        private SerializedProperty actionMapToRebindProp;
        private void OnEnable()
        {
            targetAsset = GetProp("targetAsset");
            actionMapToRebindProp = GetProp("actionMapToRebind");
            UpdatePossibleTargetMaps();
            GetSelectedActionMapIndex();
        }
        protected override void DrawGUI()
        {
            IMGUIUtil.DrawCheckableProperty(targetAsset, UpdatePossibleTargetMaps);
            DrawProp("rebinderPrefab");
            DrawProp("rectForRebinders");
            EditorGUI.BeginChangeCheck();
            selectedActionMap = EditorGUILayout.Popup(new GUIContent("Action map to rebind"), selectedActionMap, actionMaps);
            actionMapToRebindProp.stringValue = actionMaps[selectedActionMap];
            if(EditorGUI.EndChangeCheck())
            {
                CreateRebindInfos();
            }
            DrawProp("rebinds");
        }

        private void UpdatePossibleTargetMaps()
        {
            if(!targetAsset.objectReferenceValue)
            {
                return;
            }
            actionAsset = (InputActionAsset)targetAsset.objectReferenceValue;
            actionMaps = actionAsset.actionMaps.Select(map => map.name).ToArray();
        }
        private void GetSelectedActionMapIndex()
        {
            if (!actionAsset)
                return;

            var currentActionMap = TargetType.actionMapToRebind;
            for(int i = 0; i < actionMaps.Length; i++)
            {
                if(string.Equals(currentActionMap, actionMaps[i], StringComparison.Ordinal))
                {
                    selectedActionMap = i;
                    return;
                }
            }
            selectedActionMap = 0;
        }

        private void CreateRebindInfos()
        {
            TargetType.rebinds = Array.Empty<InputRebindInfo>();
            var rebinds = TargetType.rebinds;

            var inputActionReferencesThatMatchesMap = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(actionAsset))
                .OfType<InputActionReference>()
                .Where(iar => iar.name.StartsWith(actionMapToRebindProp.stringValue) && !iar.hideFlags.HasFlag(HideFlags.HideInHierarchy)) //Input system for whatever reason creates duplicates with hide in hierarchy, we want the visible ones
                .ToArray();

            foreach(var inputActionReference in inputActionReferencesThatMatchesMap)
            {
                var rebindInfo = new InputRebindInfo
                {
                    reference = inputActionReference,
                    timeout = 10,
                    excludedControls = new string[] { nameof(Mouse) }
                };
                ArrayUtility.Add(ref rebinds, rebindInfo);
            }

            TargetType.rebinds = rebinds;
            serializedObject.Update();
        }
    }
}
