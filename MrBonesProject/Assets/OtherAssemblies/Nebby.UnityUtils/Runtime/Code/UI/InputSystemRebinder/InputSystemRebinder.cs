using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

namespace Nebby.UI
{
    public class InputSystemRebinder : MonoBehaviour
    {
        public InputSystemRebinderController TiedRebinderController { get; set; }
        public RuntimeInputRebind InputRebind { get; private set; }

        [SerializeField] private Button rebindButton;
        [SerializeField] private TextMeshProUGUI inputNameText;
        private TextMeshProUGUI actionNameText;

        private void Awake()
        {
            actionNameText = rebindButton.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            actionNameText.text = InputRebind.actionName;
            inputNameText.text = InputRebind.bindingDisplayName;
        }
        public void StartRebindProcess()
        {

        }

        public void SetRebindInfo(InputRebindInfo rebindInfo)
        {
            InputRebind = new RuntimeInputRebind(rebindInfo);
        }

        public struct RuntimeInputRebind
        {
            public InputAction action;
            public float timeOut;
            public string[] excludedControls;
            public string actionName;
            public string bindingDisplayName;

            public RuntimeInputRebind(InputRebindInfo info)
            {
                action = info.reference.action;
                timeOut = info.timeout;
                excludedControls = info.excludedControls;
                actionName = info.reference.name.Split('/')[1];

                var currentBindingIndex = action.GetBindingIndexForControl(action.activeControl);
                bindingDisplayName = InputControlPath.ToHumanReadableString(
                    action.bindings[currentBindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);
            }

            public void OnActiveControlChanged()
            {
                var currentBindingIndex = action.GetBindingIndexForControl(action.activeControl);
                bindingDisplayName = InputControlPath.ToHumanReadableString(
                    action.bindings[currentBindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);
            }
        }
    }
}