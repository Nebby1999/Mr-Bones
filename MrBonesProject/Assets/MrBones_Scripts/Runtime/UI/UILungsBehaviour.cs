using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;
using UnityEngine.UI;

namespace MrBones.UI
{
    public class UILungsBehaviour : MonoBehaviour
    {
        [SerializeField] private FloatReferenceAsset maxVal;
        [SerializeField] private FloatReferenceAsset currentVal;
        [SerializeField] private Slider targetSlider;

        private void Awake()
        {
            targetSlider.maxValue = maxVal.floatValue;
            targetSlider.value = currentVal.floatValue;
        }

        private void Update()
        {
            targetSlider.value = currentVal.floatValue;
        }
    }
}
