using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Nebby
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

            int rand = UnityEngine.Random.Range(0, enumAsArray.Length);
            element = enumAsArray[rand];
            return true;
        }


        public static void ForEach<T>(this IEnumerable enumerable, Action<T> action)
        {
            foreach(T t in enumerable)
            {
                action(t);
            }
        }

        public static void AddSafely<T>(this IList<T> list, T element)
        {
            if(!list.Contains(element))
            {
                list.Add(element);
            }
        }

        public static void RemoveSafely<T>(this IList<T> list, T element)
        {
            if(list.Contains(element))
            {
                list.Remove(element);
            }
        }
    }
}