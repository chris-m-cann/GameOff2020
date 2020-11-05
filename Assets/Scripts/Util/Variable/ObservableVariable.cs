using System;
using UnityEngine;
using Util.Events;

namespace Util.Variable
{
    public class ObservableVariable<T> : Variable<T>
    {
        public event Action<T> OnEventTrigger;

        public override T Value
        {
            get => base.Value;
            set
            {
                if (base.Value.Equals(value)) return;

                base.Value = value;
                Raise(value);
            }
        }


        public event Action<T> OnValueChanged
        {
            add => OnEventTrigger += value;
            remove => OnEventTrigger -= value;
        }

        public void Raise(T t)
        {
            OnEventTrigger?.Invoke(t);
        }
    }
}