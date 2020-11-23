using System;
using System.Collections.Generic;
using System.Linq;

namespace Util
{
    public class PriorityQueue<T, TOrder> where TOrder : IComparable
    {
        private readonly List<Tuple<T, TOrder>> _elements = new List<Tuple<T, TOrder>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, TOrder priority)
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i].Item2.CompareTo(priority) > 0)
                {
                    _elements.Insert(i, Tuple.Create(item, priority));
                    return;
                }
            }

            _elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            T bestItem = _elements[0].Item1;
            _elements.RemoveAt(0);
            return bestItem;
        }

        public T Peek()
        {
            return _elements[0].Item1;
        }
    }
}