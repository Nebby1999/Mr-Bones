using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby.UnityUtils
{
    public class DestroyOnTimer : MonoBehaviour
    {
        public float timer;
        private float stopwatch;
        public void Update()
        {
            stopwatch += Time.deltaTime;
            if (stopwatch > timer)
                Destroy(gameObject);
        }
    }
}