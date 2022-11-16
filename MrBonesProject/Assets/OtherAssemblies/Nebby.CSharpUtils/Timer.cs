using System.Collections;
using UnityEngine;

namespace Nebby
{
    public class Timer
    {
        public float TimeElapsed { get; private set; }
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
        public uint Seconds { get; private set; }
        public string SecondsString
        {
            get
            {
                return Seconds <= 9 ? "0" + Seconds : Seconds.ToString();
            }
        }
        public uint DecaSeconds { get; private set; }

        public void Tick(float deltaTime)
        {
            TimeElapsed += deltaTime;
            UpdateTime();
        }

        private void UpdateTime()
        {
            Minutes = (uint)Mathf.FloorToInt(TimeElapsed / 60);
            Hours = (uint)Mathf.FloorToInt(Minutes / 60);
            Seconds = (uint)Mathf.FloorToInt(TimeElapsed % 60);
            DecaSeconds = (uint)(TimeElapsed * 10 % 10);
        }

    }
}