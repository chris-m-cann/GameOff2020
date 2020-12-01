using System;
using Luna;
using UnityEngine;

namespace Util.Inventory
{
    [RequireComponent(typeof(IProvider<Inventory>))]
    public class ResetInventory : MonoBehaviour
    {
        [SerializeField] private InventoryItem[] startingItems;
        [SerializeField] private CurrentRunData run;
        [SerializeField] private bool onlyOn1_1;



        private void Start()
        {
            if (onlyOn1_1 && run != null && run.Depth != 1) return;

            var inventory = GetComponent<IProvider<Inventory>>().Get();

            if (inventory == null) return;

            inventory.Clear();

            foreach (var item in startingItems)
            {
                item.AddToInventory(inventory);
            }
        }
    }
}