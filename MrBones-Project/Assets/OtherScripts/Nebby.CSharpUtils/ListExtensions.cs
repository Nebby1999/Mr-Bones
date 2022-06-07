using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Nebby.CSharpUtils
{
    public static class ListExtensions
    {
        public static bool TryGetRandomElement<T>(this IEnumerable<T> enumerable, out T element)
        {
            T[] enumAsArray = enumerable.ToArray();
            if (enumAsArray.Length == 0)
            {
                element = default(T);
                return false;
            }

            int rand = Random.Range(0, enumAsArray.Length);
            element = enumAsArray[rand];
            return true;
        }
    }
}