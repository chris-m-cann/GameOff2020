using System;
using UnityEngine;

namespace Util
{
    public static class VectorUtils
    {
        public static Vector3 Copy(this Vector3 self, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(
                x ?? self.x,
                y ?? self.y,
                z ?? self.z
                );
        }
        public static Vector2 ToVector2(this Vector3 self)
        {
            return new Vector2(self.x, self.y);
        }


        public static Vector2Int ToVector2Int(this Vector3 self)
        {
            return new Vector2Int(Mathf.RoundToInt(self.x), Mathf.RoundToInt(self.y));
        }

        public static bool IsCardinal(this Vector2Int self)
        {
            return !(self.x != 0 && self.y != 0) ;
        }

        public static Vector2Int CardinalNormalise(this Vector2Int self)
        {
            return new Vector2Int(
                MathUtils.ClampInt(self.x, -1, 1),
                MathUtils.ClampInt(self.y, -1, 1));
        }

        public static int CardinalMagnitude(this Vector2Int self)
        {
            return Math.Max(Math.Abs(self.x), Math.Abs(self.y));
        }
    }

    public static class Vector2IntEx
    {
        private static readonly Vector2Int _offGrid = new Vector2Int(int.MinValue, Int32.MaxValue);
        public static Vector2Int OffGrid => _offGrid;
    }
}