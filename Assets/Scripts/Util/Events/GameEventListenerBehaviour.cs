using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Util.Events
{
    public abstract class GameEventListenerBehaviour<T> : MonoBehaviour, GameEventListener<T>
    {
        [SerializeField] private EventReference<T> gameEvent;

        [SerializeField] private UltEvent<T> onEventRaised;


        private void OnEnable()
        {
            if (gameEvent == null) return;
            gameEvent.OnEventTrigger += OnEventRaised;
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;
            gameEvent.OnEventTrigger -= OnEventRaised;
        }

        public void OnEventRaised(T t)
        {
            onEventRaised.Invoke(t);
        }
    }
}