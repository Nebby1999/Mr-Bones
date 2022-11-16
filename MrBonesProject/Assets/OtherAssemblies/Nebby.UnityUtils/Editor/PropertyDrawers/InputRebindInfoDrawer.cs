using Nebby.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(InputRebindInfo))]
    public class InputRebindInfoDrawer : IMGUIPropertyDrawer
    {
        public SerializedProperty reference;
        public SerializedProperty binding;
        public SerializedProperty timeOut;
        public SerializedProperty excludedControlsArray;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GetSerializedProperties(property);

            EditorGUI.BeginProperty(position, label, property);

            var foldoutPos = new Rect(position.x, position.y, position.width, StandardPropertyHeight);
            EditorGUI.PropertyField(foldoutPos, property);

            if(property.isExpanded)
            {
                EditorGUI.indentLevel++;

                var referencePos = new Rect(position.x, position.y + StandardPropertyHeight, position.width, StandardPropertyHeight);
                EditorGUI.PropertyField(referencePos, reference);

                var timeoutPos = new Rect(referencePos.x, referencePos.yMax, referencePos.width, referencePos.height);
                EditorGUI.PropertyField(timeoutPos, timeOut);

                var excludedControlsPos = new Rect(timeoutPos.x, timeoutPos.yMax, timeoutPos.width, timeoutPos.height);
                EditorGUI.PropertyField(excludedControlsPos, excludedControlsArray);
                
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private void GetSerializedProperties(SerializedProperty parentProp)
        {
            reference = parentProp.FindPropertyRelative("reference");
            binding = parentProp.FindPropertyRelative("binding");
            timeOut = parentProp.FindPropertyRelative("timeout");
            excludedControlsArray = parentProp.FindPropertyRelative("excludedControls");
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetSerializedProperties(property);
            if(!property.isExpanded)
            {
                return StandardPropertyHeight;
            }

            var height = StandardPropertyHeight * 4;
            var excludedControlsHeight = GetPropertyHeightFromFoldout(ref excludedControlsArray);
            return height + excludedControlsHeight;
        }
    }
}
