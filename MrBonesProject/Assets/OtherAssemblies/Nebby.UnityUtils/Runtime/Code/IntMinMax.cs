using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    [Serializable]
    public struct IntMinMax
    {
        public IntMinMax(int minLimit, int maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
            min = minLimit;
            max = maxLimit;
        }
        [SerializeField] private int min, max, minLimit, maxLimit;

        public int Min => min;
        public int Max => max;
        public int MinLimit => minLimit;
        public int MaxLimit => maxLimit;
        public int GetRandomRange() 
        {
            return UnityEngine.Random.Range(Min, Max);
        } 
        public int GetRandomRangeLimits() => UnityEngine.Random.Range(MinLimit, MaxLimit);
    }
}
