using UnityEngine;

namespace Util.Inventory
{
    public abstract class InventoryItem : ScriptableObject
    {
        public InventoryKey Key;
        public Sprite Sprite;
        public bool RequiresApprovalToPickUp = false;

        public abstract bool AddToInventory(Inventory inventory);
    }
}