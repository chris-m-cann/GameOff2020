using Luna.Weapons;
using UnityEngine;

namespace Util.Inventory
{

    [CreateAssetMenu(menuName = "Custom/Inventory/Item/Weapon")]
    public class WeaponItem : InventoryItem
    {
        public Weapon Item;
        public override bool AddToInventory(Inventory inventory)
        {
            return inventory.AddItem(this);
        }
    }
}