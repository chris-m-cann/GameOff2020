using UltEvents;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Custom/Inventory")]
    public class Inventory : ScriptableObject
    {
        public UltEvent OnInventoryChanged;

        [SerializeField] private int wealth;

        public int Wealth
        {
            get => wealth;
            set
            {
                wealth = value;
                OnInventoryChanged.Invoke();
            }
        }



    }
}