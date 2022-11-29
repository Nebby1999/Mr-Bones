using Nebby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MrBones
{
    [RequireComponent(typeof(Light2D))]   
    public class FlickerLight2D : MonoBehaviour
    {
        public Light2D Light { get; private set; }

        public FloatMinMax intensityFlicker;

        private void Awake()
        {
            Light = GetComponent<Light2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Light.intensity = intensityFlicker.GetRandomRange();
        }
    }
}
