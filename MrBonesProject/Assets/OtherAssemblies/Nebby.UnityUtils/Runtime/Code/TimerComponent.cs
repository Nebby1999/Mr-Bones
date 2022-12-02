using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nebby
{
    public class TimerComponent : MonoBehaviour
    {
        public float timeElapsed;
        public uint Hours { get; private set; }
        public string HoursString
        {
            get
            {
                return Hours <= 9 ? "0" + Hours : Hours.ToString();
            }
        }
        public uint Minutes { get; private set; }
        public string MinutesString
        {
            get
            {
                return Minutes <= 9 ? "0" + Minutes : Minutes.ToString();
            }
        }
        public uint Seconds { get; private set;}
        public string SecondsString
        {
            get
            {
                return Seconds <= 9 ? "0" + Seconds : Seconds.ToString();
            }
        }
        public uint DecaSeconds { get; private set; }

        public void Update()
        {
            timeElapsed += Time.deltaTime;
            UpdateTime();
        }

        private void UpdateTime()
        {
            Minutes = (uint)Mathf.FloorToInt(timeElapsed / 60);
            Hours = (uint)Mathf.FloorToInt(Minutes / 60);
            Seconds = (uint)Mathf.FloorToInt(timeElapsed % 60);
            DecaSeconds = (uint)(timeElapsed * 10 % 10);
        }
    }
}
