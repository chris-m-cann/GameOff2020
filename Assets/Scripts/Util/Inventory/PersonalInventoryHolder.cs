using System;
using UnityEngine;

namespace Util.Inventory
{
    public class PersonalInventoryHolder : InventoryHolder
    {
        [SerializeField] private InventoryKey[] supportedKeys;


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Inventory = Inventory.CreateInstance(supportedKeys);
        }

        public override Inventory Get()
        {
            if (Inventory == null)
                Init();

            return Inventory;
        }
    }
}