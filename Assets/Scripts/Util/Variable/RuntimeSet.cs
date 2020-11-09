using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Util
{
    public class RuntimeSet<T> : ResetableObject, IEnumerable<T>
    {
        [NonSerialized] protected List<T> items = new List<T>();

        public ReadOnlyCollection<T> ListView => items.AsReadOnly();

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
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Reset(ResetScenario scenario = ResetScenario.OnDemand)
        {
            Clear();
        }
    }
}