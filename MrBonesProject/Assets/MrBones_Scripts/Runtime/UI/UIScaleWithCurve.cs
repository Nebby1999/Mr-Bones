using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MrBones
{
    public class UIScaleWithCurve : MonoBehaviour
    {
        public AnimationCurve scaleCurve;
        public bool scaleOnStart;

        private float timer;
        private bool scaling = false;
        private Transform t;
        private Keyframe firstFrame;
        private Keyframe lastFrame;

        private void Awake()
        {
            t = transform;
            firstFrame = scaleCurve.keys.First();
            lastFrame = scaleCurve.keys.Last();
        }
        private void Start()
        {
            if (scaleOnStart)
                scaling = true;
        }

        public void BeginScaling(bool reverse)
        {
            scaling = true;
            if(reverse)
            {
                timer = lastFrame.time;
            }
        }

        public void StopScaling()
        {
            scaling = false;
            timer = 0;
        }

        private void Update()
        {
            if(scaling)
            {
                timer += Time.deltaTime;
                float val = scaleCurve.Evaluate(timer);
                t.localScale = Vector3.one * val;
                if (timer > lastFrame.time)
                {
                    StopScaling();
                }
            }
        }
    }
}
