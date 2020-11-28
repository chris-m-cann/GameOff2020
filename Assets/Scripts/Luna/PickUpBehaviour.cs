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


        private void OnTriggerEnter2D(Collider2D other)
        {
            var inventory = other.GetComponent<InventoryHolder>()?.Inventory;

            if (inventory != null)
            {
                if (item.AddToInventory(inventory))
                {
                    Destroy(gameObject);
                }
            }

        }
    }
}