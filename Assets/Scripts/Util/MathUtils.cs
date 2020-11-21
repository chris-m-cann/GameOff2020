namespace Util
{
    public static class MathUtils
    {
        public static int ClampInt(int v, int min, int max)
        {
            return v < min ? min : (v > max ? max : v);
        }
    }
}