using System;
using UnityEngine;
using UltEvents;

namespace Util.Inventory
{
    [RequireComponent(typeof(IProvider<Inventory>))]
    public class InventoryAggregateObserverBehaviour : MonoBehaviour
    {
        [SerializeField] private InventoryKey key;
        [SerializeField] private string stringTemplate = "{0}";


        [SerializeField] private UltEvent<int> onUpdate;
        [SerializeField] private UltEvent<bool> onUpdateHasResource;
        [SerializeField] private UltEvent<string> onUpdateString;


        private Inventory _inventory;


        private void OnEnable()
        {
            _inventory = GetComponent<IProvider<Inventory>>().Get();

            if (_inventory == null)
            {
                Debug.LogError("No Inventory Found");
                return;
            }

            RaiseEvents();
            _inventory.OnInventoryChanged += OnUpdate;
        }

        private void OnDisable()
        {
            if (_inventory == null) return;
            _inventory.OnInventoryChanged -= OnUpdate;
        }

        private void RaiseEvents()
        {
            AggregateSlot slot;
            if (_inventory.RetrieveSlot(key, out slot))
            {
                onUpdate.Invoke(slot.Total);
                onUpdateHasResource.Invoke(slot.Total != 0);
                onUpdateString.Invoke(String.Format(stringTemplate, slot.Total.ToString()));
            }

        }

        private void OnUpdate(InventoryKey key)
        {
            if (key == this.key)
            {
                RaiseEvents();
            }
        }
    }
}