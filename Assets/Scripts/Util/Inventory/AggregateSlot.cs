namespace Util.Inventory
{
    public class AggregateSlot : InventorySlot<AggregateItem>
    {
        public int Total;

        public override void Apply(AggregateItem item)
        {
            Total += item.Amount;
        }
    }
}