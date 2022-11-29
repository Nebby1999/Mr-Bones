using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor.Windows
{
    public class FloatMinMaxLimitPopup : PopupWindowContent
    {
        public FloatMinMaxLimitPopup(SerializedProperty minLimit, SerializedProperty maxLimit, SerializedProperty min, SerializedProperty max)
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
            minLimitProp.floatValue = EditorGUILayout.FloatField("Min Limit", minLimitProp.floatValue);
            maxLimitProp.floatValue = EditorGUILayout.FloatField("Max Limit", maxLimitProp.floatValue);

            GUILayout.Label("Min and Max", EditorStyles.boldLabel);
            minProp.floatValue = EditorGUILayout.FloatField("Min", minProp.floatValue);
            maxProp.floatValue = EditorGUILayout.FloatField("Max", maxProp.floatValue);
            minLimitProp.serializedObject.ApplyModifiedProperties();
        }
    }
}
