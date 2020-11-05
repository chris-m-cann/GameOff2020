using System;
using UnityEngine;
using Util.Events;

namespace Util.Variable
{
    [Serializable]
    public class VariableReference<T>
    {
        [SerializeField] private Variable<T> variable;
        [SerializeField] private ObservableVariable<T> observable;
        [SerializeField] private T constant;
        [SerializeField] private byte option;

        public T Value
        {
            get
            {
                switch (option)
                {
                    case 0: return variable.Value;
                    case 1: return observable.Value;
                    default: return constant;
                }
            }

            set
            {
                switch (option)
                {
                    case 0:
                        variable.Value = value;
                        break;
                    case 1:
                        observable.Value = value;
                        break;
                    default:
                        constant = value;
                        break;
                }
            }
        }


        public static implicit operator T(VariableReference<T> reference)
        {
            return reference.Value;
        }
    }
}