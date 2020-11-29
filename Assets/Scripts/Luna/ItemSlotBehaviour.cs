using System;
using Luna.Weapons;
using UnityEngine;
using Util;
using Util.Inventory;

namespace Luna
{
    [RequireComponent(typeof(IProvider<Inventory>))]
    public class ItemSlotBehaviour : MonoBehaviour
    {
        [SerializeField] private InventoryKey itemKey;
        [SerializeField] private PickUpBehaviour pickupPrefab;

        private Inventory _inventory;
        private WeaponItem _item;
        private PickUpBehaviour _pickUp;

        private ItemSlotUiController _ui;

        private void Start()
        {
            UpdateItem(itemKey);
        }

        private void OnEnable()
        {
            _inventory = GetComponent<IProvider<Inventory>>().Get();
            if (_inventory)
            {
                _inventory.OnInventoryChanged += UpdateItem;
            }

            _ui = FindObjectOfType<ItemSlotUiController>();
            if (_ui)
            {
                _ui.OnButtonDown += OnButtonClicked;
            }
        }

        private void OnDisable()
        {
            if (_inventory)
            {
                _inventory.OnInventoryChanged -= UpdateItem;
            }

            if (_ui)
            {
                _ui.OnButtonDown -= OnButtonClicked;
            }
        }

        private void UpdateItem(InventoryKey key)
        {
            if (key != itemKey) return;

            WeaponSlot slot;
            if (_inventory.RetrieveSlot(itemKey, out slot))
            {
                _item = slot.Item;
            }
        }

        private void OnButtonClicked()
        {
            bool hasItem = _item != null;
            bool isOnPickup = _pickUp != null;

            if (hasItem && isOnPickup)
            {
                _pickUp.Swap(_item);
            }
            else if (hasItem && !isOnPickup)
            {
                // drop item
                DropItem();
            }
            else if (!hasItem && isOnPickup)
            {
               _pickUp.PickUp(_inventory);
            }
        }

        private void DropItem()
        {
            var pickUp = Instantiate(pickupPrefab, transform.position, Quaternion.identity)
                .GetComponent<PickUpBehaviour>();

            pickUp.SetItem(_item);
            _item = null;
            OnEnterPickup(pickUp);
        }

        public void OnEnterPickup(PickUpBehaviour pickUpBehaviour)
        {
            if (!pickUpBehaviour.PickUpNeedApproval)
            {
               pickUpBehaviour.PickUp(_inventory);
               return;
            }

            bool hasItem = _item != null;

            if (hasItem)
            {
                _ui.ChangeState(ItemSlotUiController.State.Swap);
            }
            else
            {
                _ui.ChangeState(ItemSlotUiController.State.PickUp);
            }

            _pickUp = pickUpBehaviour;
        }

        public void OnExitPickup(PickUpBehaviour pickUpBehaviour)
        {
            bool hasItem = _item != null;

            if (hasItem)
            {
                _ui.ChangeState(ItemSlotUiController.State.DROP);
            }
            else
            {
                _ui.ChangeState(ItemSlotUiController.State.DISABLED);
            }

            _pickUp = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var pickup = other.GetComponent<PickUpBehaviour>();

            if (pickup == null) return;


            OnEnterPickup(pickup);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var pickup = other.GetComponent<PickUpBehaviour>();

            if (pickup == null) return;


            OnExitPickup(pickup);
        }
    }
}