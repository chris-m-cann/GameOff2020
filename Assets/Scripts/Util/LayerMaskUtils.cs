using UnityEngine;

namespace Util
{
    public static class LayerMaskUtils
    {
        public static bool Contains(this LayerMask self, int layer)
        {
            var layerBit = 1 << layer;

            return (self.value & layerBit) == layerBit;
        }
    }
}