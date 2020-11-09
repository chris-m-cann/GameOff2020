using UnityEngine;
using System.Collections.Generic;

namespace Ai
{
    public class Blackboard : ScriptableObject
    {
        public struct Element
        {
            public string description;
            public object data;

            public Element(object d, string desc = "")
            {
                data = d;
                description = desc;
            }
        }

        public readonly struct ElementKey
        {
            public readonly int keyHash;
            public ElementKey(int hash) => keyHash = hash;
        }

        private Dictionary<int, Element> _table = new Dictionary<int, Element>();

        public static ElementKey StringToKey(string key) => new ElementKey(key.GetHashCode()); 

        public Element? Retrieve(string key) => Retrieve(key.GetHashCode());
        
        public Element? Retrieve(ElementKey key) => Retrieve(key.keyHash);

        public T RetrieveData<T>(ElementKey key) => (T) (Retrieve(key)?.data);
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

        public void Add(ElementKey key, Element data) => Add(key.keyHash, data);
        public void Add(string key, Element data) => Add(StringToKey(key), data);
        public void Add<T>(ElementKey key, T data) => Add(key.keyHash, new Element(data));
        public void Add<T>(string key, T data) => Add(StringToKey(key), new Element(data));
        

        private void Add(int key, Element data)
        {
            _table[key] = data;
        }
    }
}