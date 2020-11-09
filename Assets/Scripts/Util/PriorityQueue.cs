using System;
using System.Collections.Generic;

namespace Util
{
    public class PriorityQueue<T, TOrder> where TOrder : IComparable
    {
        private readonly List<Tuple<T, TOrder>> _elements = new List<Tuple<T, TOrder>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, TOrder priority)
        {
            _elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < _elements.Count; i++) {
                if (_elements[i].Item2.CompareTo(_elements[bestIndex].Item2) < 0) {
                    bestIndex = i;
                }
            }

            T bestItem = _elements[bestIndex].Item1;
            _elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}