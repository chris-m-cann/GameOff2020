using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class RuntimeSet<T> : ScriptableObject
    {
        [SerializeField] protected List<T> items = new List<T>();

        public void Add(T thing)
        {
            if (!items.Contains(thing))
                items.Add(thing);
        }

        public void Remove(T thing)
        {
            if (items.Contains(thing))
                items.Remove(thing);
        }

        public void ForEach(Action<T> block)
        {
            foreach (var item in items)
            {
                block.Invoke(item);
            }
        }

        public void Clear() => items.Clear();
    }
}