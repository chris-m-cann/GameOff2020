using System;
using System.Collections.Generic;
using System.Linq;
using UltEvents;
using UnityEngine;

namespace Util.Inventory
{
    [CreateAssetMenu(menuName = "Custom/Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        public event Action<InventoryKey> OnInventoryChanged;

        [SerializeField] private InventoryKey[] supportedItems;


        private readonly Dictionary<InventoryKey, AggregateSlot> _aggregates = new Dictionary<InventoryKey, AggregateSlot>();

        public void Init(InventoryKey[] supportedKeys)
        {
            var unsupportedKeys = _aggregates.Keys.Where(it => Array.IndexOf(supportedKeys, it) < 0);

            foreach (var key in unsupportedKeys)
            {
                _aggregates.Remove(key);
            }

            supportedItems = supportedKeys;
        }

        private void OnEnable()
        {
            Init(supportedItems);
        }

        private void OnValidate()
        {
            Init(supportedItems);
        }

        bool IsKeySupported(InventoryItem item)
        {
            return !(Array.IndexOf(supportedItems, item.Key) < 0);
        }

        public bool AddAggregate(AggregateItem item)
        {
            return AddItem(item, _aggregates, () => new AggregateSlot());
        }


        private bool AddItem<T1, T2>(T1 item, Dictionary<InventoryKey, T2> slots, Func<T2> createNewSlot)
            where T1 : InventoryItem
            where T2 : InventorySlot<T1>
        {
            if (!IsKeySupported(item)) return false;

            if (slots.ContainsKey(item.Key))
            {
                slots[item.Key].Apply(item);
            }
            else
            {
                var slot = createNewSlot();
                slot.Apply(item);
                slots.Add(item.Key, slot);
            }

            OnInventoryChanged.Invoke(item.Key);
            return true;
        }

        public bool RetrieveSlot(InventoryKey key, out AggregateSlot slot)
        {
            return _aggregates.TryGetValue(key, out slot);
        }


        public static Inventory CreateInstance(InventoryKey[] supportedKeys)
        {
            var instance = ScriptableObject.CreateInstance<Inventory>();
            instance.Init(supportedKeys);
            return instance;
        }
    }
}