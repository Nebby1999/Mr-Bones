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
        public FloatMinMaxLimitPopup(SerializedProperty min, SerializedProperty max)
        {
            minProp = min;
            maxProp = max;
        }
        SerializedProperty minProp;
        SerializedProperty maxProp;
        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Min Max Limits", EditorStyles.boldLabel);
            minProp.floatValue = EditorGUILayout.FloatField("Min Limit", minProp.floatValue);
            maxProp.floatValue = EditorGUILayout.FloatField("Max Limit", maxProp.floatValue);
            minProp.serializedObject.ApplyModifiedProperties();
        }
    }
}
