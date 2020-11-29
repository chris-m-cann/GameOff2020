using System;
using UnityEngine;
using UltEvents;

namespace Util.Inventory
{
    [RequireComponent(typeof(IProvider<Inventory>))]
    public class InventoryWeaponObserverBehaviour : MonoBehaviour
    {
        [SerializeField] private InventoryKey key;
        [SerializeField] private string stringTemplate = "{0}";


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
            WeaponSlot slot;
            if (_inventory.RetrieveSlot(key, out slot))
            {
                onUpdateHasResource.Invoke(slot.Item != null);
                onUpdateString.Invoke(String.Format(stringTemplate, slot.Item?.name ?? "none"));
            }
            else
            {
                onUpdateHasResource.Invoke(false);
                onUpdateString.Invoke(String.Format(stringTemplate, "none"));
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