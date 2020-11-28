using UnityEngine;

namespace Util.Inventory
{
    [CreateAssetMenu(menuName = "Custom/Inventory/Item/Aggregate")]
    public class AggregateItem : InventoryItem
    {
        public int Amount = 0;


        public override bool AddToInventory(Inventory inventory)
        {
            return inventory.AddAggregate(this);
        }
    }
}