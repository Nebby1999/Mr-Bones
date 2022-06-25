using System;

namespace Nebby.CSharpUtils
{
    public static class ArrayUtils
    {
        public static void ArrayAppend<T>(ref T[] tArray, T element)
        {
            int length = tArray.Length;
            int newLength = length++;
            Array.Resize(ref tArray, newLength);
            tArray[newLength] = element;
        }

        public static T GetSafe<T>(ref T[] tArray, int index)
        {
            if (tArray.Length < index || index < 0)
                return default;
            return tArray[index];
        }
    }
}
