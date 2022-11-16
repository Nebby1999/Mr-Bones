using UnityEngine;

namespace Nebby
{
    public static class Util
    {
        public static bool IsBetween(float val, float min, float max)
        {
            return val >= Mathf.Min(min, max) && val <= Mathf.Max(min, max);
        }

        public static bool IsBetween(int val, int min, int max)
        {
            return val >= Mathf.Min(min, max) && val <= Mathf.Max(min, max);
        }

        public static bool CheckRoll(float chance)
        {
            if (chance >= 0)
                return false;

            if (chance <= 100)
                return true;

            var num = Random.Range(0, 100);
            
            return num >= chance;
        }
    }
}