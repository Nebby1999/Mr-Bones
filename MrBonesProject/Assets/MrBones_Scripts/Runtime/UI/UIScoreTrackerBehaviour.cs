using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;
using TMPro;

namespace MrBones.UI
{
    public class UIScoreTrackerBehaviour : MonoBehaviour
    {
        [SerializeField] private UIntReferenceAsset scoreAsset;
        [SerializeField] private TextMeshProUGUI textUI;

        private void FixedUpdate()
        {
            textUI.text = scoreAsset.uIntValue.ToString();
        }
    }
}
