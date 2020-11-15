using System;
using UnityEngine;

namespace Util
{
    public class AddComponentToRuntimeSet<T> : MonoBehaviour
    {
        [SerializeField] private RuntimeSet<T> set;

        private void Awake()
        {
            var component = GetComponent<T>();
            if (component != null)
            {
                set.Add(component);
            }
        }

        private void OnDestroy()
        {
            var component = GetComponent<T>();
            if (component != null)
            {
                set.Remove(component);
            }
        }
    }
}