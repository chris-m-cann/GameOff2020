using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Util
{
    public static class CollectionUtils
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


        public static void AddNullableRange<T>(this List<T> self, IEnumerable<T> range)
        {
            if (range == null) return;

            self.AddRange(range);
        }
    }
}