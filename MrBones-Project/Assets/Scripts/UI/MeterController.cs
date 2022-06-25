using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using UnityEngine.UI;

namespace MrBones.UI
{
    public class MeterController : MonoBehaviour
    {
        public Slider slider;
        public FloatReference currentValue;
        public FloatReference maxValue;
        public bool useGradient;
        public Gradient colorGradient;

        private Image Fill { get; set; }

        private void Awake()
        {
            Fill = slider.fillRect.GetComponent<Image>();
        }
        private void Update()
        {
            slider.maxValue = maxValue.Value;
            slider.value = currentValue.Value;

            if (useGradient)
            {
                Fill.color = colorGradient.Evaluate(slider.normalizedValue);
            }
        }
    }
}