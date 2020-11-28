namespace Util.Inventory
{
    public abstract class InventorySlot<T> where T : InventoryItem
    {
        public abstract void Apply(T item);
    }
}