using System;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Luna
{
    public class ItemSlotUiController : MonoBehaviour
    {
        public enum State
        {
            Disabled, PickUp, Drop, Swap
        }

        public event Action OnButtonDown;
        [SerializeField] private UltEvent<string> onStateChange;


        [SerializeField] private Button button;
        [SerializeField] private string pickupDisplay;
        [SerializeField] private string dropDisplay;
        [SerializeField] private string swapDisplay;


        public void TriggerOnButtonDown()
        {
            OnButtonDown?.Invoke();
        }

        public void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.Disabled:
                    button.interactable = false;
                    break;
                case State.PickUp:
                    button.interactable = true;
                    onStateChange.Invoke(pickupDisplay);
                    break;
                case State.Drop:
                    button.interactable = true;
                    onStateChange.Invoke(dropDisplay);
                    break;
                case State.Swap:
                    button.interactable = true;
                    onStateChange.Invoke(swapDisplay);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
    }
}