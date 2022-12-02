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

        public bool Scaling { get; private set; }
        private float timer;
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
                Scaling = true;
        }

        public void BeginScaling()
        {
            Scaling = true;
        }

        public void StopScaling()
        {
            Scaling = false;
            timer = 0;
        }

        private void Update()
        {
            if(Scaling)
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
