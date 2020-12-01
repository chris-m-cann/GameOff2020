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

        public static WeaponItem CreateInstance(
            InventoryKey key,
            Sprite sprite,
            Weapon weapon,
            bool requiresApprovalToPickUp = false)
        {
            var instance = CreateInstance<WeaponItem>();
            instance.Key = key;
            instance.Sprite = sprite;
            instance.RequiresApprovalToPickUp = requiresApprovalToPickUp;
            instance.Item = weapon;

            return instance;
        }
    }
}