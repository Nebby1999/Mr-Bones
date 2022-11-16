using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

namespace Nebby.UI
{
    public class InputSystemRebinderController : MonoBehaviour
    {
        public InputActionAsset targetAsset;
        public InputSystemRebinder rebinderPrefab;
        public RectTransform rectForRebinders;
        public string actionMapToRebind;

        public InputRebindInfo[] rebinds = Array.Empty<InputRebindInfo>();

        private void Awake()
        {
            foreach(InputRebindInfo rebindInfo in rebinds)
            {
                var rebinderInstance = Instantiate(rebinderPrefab.gameObject, rectForRebinders);
                var inputSystemRebinder = rebinderInstance.GetComponent<InputSystemRebinder>();
                inputSystemRebinder.TiedRebinderController = this;
                inputSystemRebinder.SetRebindInfo(rebindInfo);
            }
        }
    }

    [Serializable]
    public struct InputRebindInfo
    {
        public InputActionReference reference;
        public string binding;
        public float timeout;
        public string[] excludedControls;
    }
}
