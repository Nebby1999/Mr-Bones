using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nebby.Editor;

namespace Nebby.Editor.Inspectors
{
    [CustomEditor(typeof(RotateObject))]
    public class RotateObjectInspector : IMGUIInspector<RotateObject>
    {
        SerializedProperty useVector3ForRotation;
        public void OnEnable()
        {
            useVector3ForRotation = GetProp(nameof(useVector3ForRotation));
        }

        protected override void DrawGUI()
        {
            DrawProp(useVector3ForRotation);
            if(useVector3ForRotation.boolValue)
            {
                DrawProp(nameof(RotateObject.rotationSpeed));
            }
            else
            {
                DrawProp(nameof(RotateObject.rotateX));
                DrawProp(nameof(RotateObject.rotateY));
                DrawProp(nameof(RotateObject.rotateZ));
                DrawProp(nameof(RotateObject.speed));
            }
        }
    }
}
