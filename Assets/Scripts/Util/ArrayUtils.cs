using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Util
{
    public static class ArrayUtils
    {
        public static void DeleteRange<T>(ref T[] self, int fromInclusive, int toExclusive)
        {
            var diff = toExclusive - fromInclusive;

            for (int i = fromInclusive; i < self.Length - diff; i++)
            {
                // moving elements downwards, to fill the gap at [index]
                self[i] = self[i + diff];
            }
            Array.Resize(ref self, self.Length - diff);
        }

        public static T RandomElement<T>(this T[] self)
        {
            return self[Random.Range(0, self.Length)];
        }


    }
}