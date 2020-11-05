using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(Rigidbody))]
    public class SetRigidbodyVelocity : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetVelocity(Vector3 v) => _rigidbody.velocity = v;
    }
}