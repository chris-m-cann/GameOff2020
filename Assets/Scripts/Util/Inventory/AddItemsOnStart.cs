using System;
using UnityEngine;

namespace Util.Inventory
{
    [RequireComponent(typeof(IProvider<Inventory>))]
    public class AddItemsOnStart : MonoBehaviour
    {
        [SerializeField] private InventoryItem[] items;


        private void Start()
        {
            var inventory = GetComponent<IProvider<Inventory>>().Get();

            if (inventory == null) return;

            foreach (var item in items)
            {
                item.AddToInventory(inventory);
            }
        }
    }
}