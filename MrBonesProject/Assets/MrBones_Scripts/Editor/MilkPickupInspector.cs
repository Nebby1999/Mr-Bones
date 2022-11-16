using System.Collections;
using UnityEngine;
using Nebby.Editor;
using UnityEditor;
using UnityEditor.Events;
using MrBones.Pickups;

namespace MrBones.Editor
{
    [CustomEditor(typeof(MilkPickup))]
    public class MilkPickupInspector : IMGUIInspector<MilkPickup>
    {
        protected override void DrawGUI()
        {
            DrawDefaultInspector();

            if(GUILayout.Button("Autofill onPickedUp Event"))
            {
                AutoFillOnPickedUpEvent();
            }
        }

        private void AutoFillOnPickedUpEvent()
        {
            var stageController = FindObjectOfType<Stages.StageController>();
            if(stageController)
            {
                UnityEventTools.AddVoidPersistentListener(TargetType.onPickedUp, new UnityEngine.Events.UnityAction(stageController.OnMilkCollected));
                PrefabUtility.RecordPrefabInstancePropertyModifications(TargetType);
            }
        }
    }
}