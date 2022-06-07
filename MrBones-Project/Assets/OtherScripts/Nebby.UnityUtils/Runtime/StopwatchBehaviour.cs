using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Nebby.UnityUtils
{
    public class StopwatchBehaviour : MonoBehaviour
    {
        public float interval;
        public bool isFixed;
        public bool looping;
        public int maxLoops;
        public UnityEvent action;

        private float time;
        private int loops;

        public void OnEnable()
        {
            loops = 0;
        }

        private void Update()
        {
            if (isFixed)
                return;

            time += Time.deltaTime;
            if (time > interval)
                RunAction();
        }

        private void FixedUpdate()
        {
            if (!isFixed)
                return;

            time += Time.fixedDeltaTime;
            if (time > interval)
                RunAction();
        }

        private void RunAction()
        {
            action?.Invoke();

            //Not looping? disable and return
            if (!looping)
            {
                enabled = false;
                time = 0;
                return;
            }

            //If max loops is less than 0, then it means it'll always loop, just set time back to 0;
            if (maxLoops < 0)
            {
                time = 0;
                return;
            }

            time = 0;
            loops++;
            //If loops is larger than max loops, disable
            if (loops >= maxLoops)
                enabled = false;
        }
    }
}
