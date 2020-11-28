using UnityEngine;

namespace Util.Inventory
{
    [CreateAssetMenu(menuName = "Custom/Inventory/Key")]
    public class InventoryKey : ScriptableObject
    {
        [HideInInspector]
        public int Key;

        private void OnEnable()
        {
            Refresh();
        }

        private void OnValidate()
        {
            Refresh();
        }

        private void Refresh()
        {
            Key = ("InventoryKey." + name).GetHashCode();
        }
    }
}