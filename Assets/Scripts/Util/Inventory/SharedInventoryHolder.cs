using System;
using UnityEngine;

namespace Util.Inventory
{
    public class SharedInventoryHolder : InventoryHolder
    {
        [SerializeField] private Inventory inventory;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Inventory = inventory;
        }

        public override Inventory Get()
        {
            if (Inventory == null)
                Init();

            return Inventory;
        }
    }
}