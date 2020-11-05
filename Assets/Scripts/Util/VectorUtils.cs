using UnityEngine;

namespace Util
{
    public static class VectorUtils
    {
        public static Vector3 copy(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(
                x ?? v.x,
                y ?? v.y,
                z ?? v.z
                );
        }
        public static Vector2 toVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }


        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

    }
}