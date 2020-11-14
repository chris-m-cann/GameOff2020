using System.Collections.Generic;

namespace Util
{
    public static class DictionaryUtils
    {
        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            TV value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TV GetValueOrNull<TK, TV>(this IDictionary<TK, TV> dict, TK key) where TV : class
        {
            TV value;
            dict.TryGetValue(key, out value);
            return value;
        }
    }
}