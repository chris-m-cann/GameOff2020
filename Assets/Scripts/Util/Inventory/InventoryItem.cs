using UnityEngine;

namespace Util.Inventory
{
    public abstract class InventoryItem : ScriptableObject
    {
        public InventoryKey Key;
        public Sprite Sprite;

        public abstract bool AddToInventory(Inventory inventory);
    }
}