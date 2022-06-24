using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using Nebby.UnityUtils.Editor.Utils;
using Nebby.UnityUtils.Editor.Core.Inspectors;

namespace Nebby.UnityUtils.Editor.Inspectors
{
    [CustomEditor(typeof(SoundDef))]
    public class SoundDefInspector : Inspector<SoundDef>
    {
        VisualElement inspectorDataContainer;
        VisualElement pitchContainer;
        VisualElement volumeContainer;
        protected override void OnEnable()
        {
            base.OnEnable();
            inspectorDataContainer = RootVisualElement.Q<VisualElement>("InspectorDataContainer");
            volumeContainer = inspectorDataContainer.Q<VisualElement>("VolumeContainer");
            pitchContainer = inspectorDataContainer.Q<VisualElement>("PitchContainer");
        }
        protected override void FinalizeInspectorGUI()
        {
            AddSimpleContextMenu(volumeContainer, new ContextMenuData
            {
                menuName = "Uses Constant Value",
                menuAction = SetVolumeBoolAndUpdateFlex,
                actionStatusCheck = x => TargetType.volumeIsConstant ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal
            });
            volumeContainer.Q<VisualElement>("constantVolume").SetDisplay(TargetType.volumeIsConstant);
            volumeContainer.Q<VisualElement>("volume").SetDisplay(!TargetType.volumeIsConstant);

            AddSimpleContextMenu(pitchContainer, new ContextMenuData
            {
                menuName = "Uses Constant Value",
                menuAction = SetPitchBoolAndUpdateFlex,
                actionStatusCheck = x => TargetType.pitchIsConstant ? DropdownMenuAction.Status.Checked : DropdownMenuAction.Status.Normal
            });
            pitchContainer.Q<VisualElement>("constantPitch").SetDisplay(TargetType.pitchIsConstant);
            pitchContainer.Q<VisualElement>("pitch").SetDisplay(!TargetType.pitchIsConstant);
        }

        private void SetVolumeBoolAndUpdateFlex(DropdownMenuAction dma)
        {
            SetBoolAndUpdateFlex(ref TargetType.volumeIsConstant, volumeContainer.Q<PropertyField>("constantVolume"), volumeContainer.Q<PropertyField>("volume"));
        }

        private void SetPitchBoolAndUpdateFlex(DropdownMenuAction dma)
        {
            SetBoolAndUpdateFlex(ref TargetType.pitchIsConstant, pitchContainer.Q<PropertyField>("constantPitch"), pitchContainer.Q<PropertyField>("pitch"));
        }
        private void SetBoolAndUpdateFlex(ref bool boolean, VisualElement shownIfTrue, VisualElement shownIfFalse)
        {
            boolean = !boolean;

            shownIfTrue.SetDisplay(boolean);
            shownIfFalse.SetDisplay(!boolean);
        }
    }
}