using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    [Serializable]
    public class FloatMinMax
    {
        public FloatMinMax()
        {
            min = 5;
            max = 10;
            minLimit = 0;
            maxLimit = 15;
        }

        public FloatMinMax(float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
            min = minLimit;
            max = maxLimit;
        }
        [SerializeField] private float min, max, minLimit, maxLimit;

        public float Min => min;
        public float Max => max;
        public float MinLimit => minLimit;
        public float MaxLimit => maxLimit;
        public float GetRandomRange() 
        {
            return UnityEngine.Random.Range(Min, Max);
        } 
        public float GetRandomRangeLimits() => UnityEngine.Random.Range(MinLimit, MaxLimit);
    }
}
