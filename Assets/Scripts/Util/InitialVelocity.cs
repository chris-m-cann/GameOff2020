using System;
using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(Rigidbody))]
    public class InitialVelocity : MonoBehaviour
    {
        [SerializeField] private Vector3 velocity;

        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}