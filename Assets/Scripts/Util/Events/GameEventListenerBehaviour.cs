using UnityEngine;
using UnityEngine.Events;

namespace Util.Events
{
    public abstract class GameEventListenerBehaviour<T, TEvent, TUnityEvent> : MonoBehaviour, GameEventListener<T>
    where TEvent : GameEvent<T> where TUnityEvent : UnityEvent<T>
    {
        [SerializeField] private EventReference<T> gameEvent;

        [SerializeField] private TUnityEvent onEventRaised;


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