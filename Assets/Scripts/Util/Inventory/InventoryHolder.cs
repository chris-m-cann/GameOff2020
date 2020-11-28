using UnityEngine;

namespace Util.Inventory
{
    public abstract class InventoryHolder : MonoBehaviour, IProvider<Inventory>
    {
        public Inventory Inventory { get; protected set; }

        public abstract Inventory Get();
    }
}