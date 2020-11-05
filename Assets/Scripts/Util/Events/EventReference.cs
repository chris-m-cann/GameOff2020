using System;
using UnityEngine;
using Util.Variable;

namespace Util.Events
{
    [Serializable]
    public class EventReference<T>
    {
        [SerializeField] private GameEvent<T> gameEvent;
        [SerializeField] private ObservableVariable<T> observable;
        [SerializeField] private byte option;

        public event Action<T> OnEventTrigger
        {
            add
            {
                if (option == 0 && gameEvent != null)
                {
                    gameEvent.OnEventTrigger += value;
                }
                else if (observable != null)
                {
                    observable.OnEventTrigger += value;
                }
            }
            remove
            {
                if (option == 0 && gameEvent != null)
                {
                    gameEvent.OnEventTrigger -= value;
                }
                else if (observable != null)
                {
                    observable.OnEventTrigger -= value;
                }
            }
        }

    }

}