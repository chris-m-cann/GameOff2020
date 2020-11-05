using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Events
{
    public class GameEvent<T> : ScriptableObject
    {
        public event Action<T> OnEventTrigger;

        #if UNITY_EDITOR
        public T ValueToRaise;
        #endif

        public void Raise(T t)
        {
            OnEventTrigger?.Invoke(t);
        }
    }

}