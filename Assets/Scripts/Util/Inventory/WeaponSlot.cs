using Luna.Weapons;

namespace Util.Inventory
{
    public class WeaponSlot : InventorySlot<WeaponItem>
    {
        public WeaponItem Item;
        public override void Apply(WeaponItem item)
        {
            Item = item;
        }
    }
}