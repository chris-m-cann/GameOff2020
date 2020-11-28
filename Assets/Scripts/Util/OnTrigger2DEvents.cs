using System;
using UltEvents;
using UnityEngine;

namespace Util
{
    public class OnTrigger2DEvents : MonoBehaviour
    {
        [SerializeField] private LayerMask layers;
        [SerializeField] private UltEvent<Collider2D> onTriggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (layers.Contains(other.gameObject.layer))
            {
                onTriggerEnter.Invoke(other);
            }
        }
    }
}