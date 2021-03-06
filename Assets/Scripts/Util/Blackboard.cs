using System;
using UnityEngine;
using System.Collections.Generic;
using Util;

namespace Ai
{
    public class Blackboard : ScriptableObject
    {
        public struct Element
        {
            public string Description;
            public object Data;

            public Element(object d, string desc = "")
            {
                Data = d;
                Description = desc;
            }
        }

        [Serializable]
        public readonly struct ElementKey
        {
            public readonly int KeyHash;
            public ElementKey(int hash) => KeyHash = hash;
        }

        private Dictionary<int, Element> _table = new Dictionary<int, Element>();

        public static ElementKey StringToKey(string key) => new ElementKey(key.GetHashCode()); 

        public Element? Retrieve(string key) => Retrieve(key.GetHashCode());
        
        public Element? Retrieve(ElementKey key) => Retrieve(key.KeyHash);
        public Element? Retrieve(BlackboardKey key) => Retrieve(key.Key.KeyHash);

        public T RetrieveData<T>(ElementKey key) => (T) (Retrieve(key)?.Data);

        public T RetrieveData<T>(BlackboardKey key) => (T) (Retrieve(key)?.Data);
        public T RetrieveData<T>(string key) => RetrieveData<T>(StringToKey(key));
        
        private Element? Retrieve(int hash)
        {
            if (_table.TryGetValue(hash, out var element))
            {
                return element;
            }
            else
            {
                return null;
            }
        }

        public void Add(ElementKey key, Element data) => Add(key.KeyHash, data);

        public void Add(BlackboardKey key, Element data) => Add(key.Key.KeyHash, data);
        public void Add(string key, Element data) => Add(StringToKey(key), data);
        public void Add<T>(ElementKey key, T data) => Add(key.KeyHash, new Element(data));

        public void Add<T>(BlackboardKey key, T data) => Add(key.Key.KeyHash, new Element(data));
        public void Add<T>(string key, T data) => Add(StringToKey(key), new Element(data));
        

        private void Add(int key, Element data)
        {
            _table[key] = data;
        }

        public void Remove(ElementKey key) => _table.Remove(key.KeyHash);
        public void Remove(BlackboardKey key) => _table.Remove(key.Key.KeyHash);
        public void Remove(string key) => Remove(StringToKey(key));

        public bool Contains(BlackboardKey key) => Contains(key.Key);
        public bool Contains(ElementKey key) => Contains(key.KeyHash);
        public bool Contains(string key) => Contains(StringToKey(key));
        private bool Contains(int key) => _table.ContainsKey(key);
    }
}