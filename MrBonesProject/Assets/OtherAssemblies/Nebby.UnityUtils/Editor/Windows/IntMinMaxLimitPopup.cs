using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor.Windows
{
    public class IntMinMaxLimitPopup : PopupWindowContent
    {
        public IntMinMaxLimitPopup(SerializedProperty minLimit, SerializedProperty maxLimit, SerializedProperty min, SerializedProperty max)
        {
            minLimitProp = minLimit;
            maxLimitProp = maxLimit;
            minProp = min;
            maxProp = max;
        }
        SerializedProperty minLimitProp;
        SerializedProperty minProp;
        SerializedProperty maxProp;
        SerializedProperty maxLimitProp;
        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Min Max Limits", EditorStyles.boldLabel);
            minLimitProp.intValue = EditorGUILayout.IntField("Min Limit", minLimitProp.intValue);
            maxLimitProp.intValue = EditorGUILayout.IntField("Max Limit", maxLimitProp.intValue);

            GUILayout.Label("Min and Max", EditorStyles.boldLabel);
            minProp.intValue = EditorGUILayout.IntField("Min", minProp.intValue);
            maxProp.intValue = EditorGUILayout.IntField("Max", maxProp.intValue);
            minLimitProp.serializedObject.ApplyModifiedProperties();
        }
    }
}
