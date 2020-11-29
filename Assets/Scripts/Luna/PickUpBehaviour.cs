using System;
using UnityEngine;
using Util.Inventory;

namespace Luna
{
    [RequireComponent(typeof(Collider2D))]
    public class PickUpBehaviour : MonoBehaviour
    {
        [SerializeField] private InventoryItem item;
        [SerializeField] private SpriteRenderer sprite;

        public bool PickUpNeedApproval => item?.RequiresApprovalToPickUp ?? false;

        private void Awake()
        {
            Init();
        }

        private void OnValidate()
        {
            Init();
        }

        private void Init()
        {
            if (sprite && item.Sprite)
            {
                sprite.sprite = item.Sprite;
            }
        }

        public void SetItem(InventoryItem item)
        {
            this.item = item;
            Init();
        }

        public void PickUp(Inventory inventory)
        {
            if (inventory != null)
            {
                if (item.AddToInventory(inventory))
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Swap(InventoryItem item)
        {
            if (item != null)
            {
                this.item = item;
                Init();
            }
        }
    }
}