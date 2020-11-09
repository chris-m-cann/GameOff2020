using System;
using UltEvents;
using UnityEngine;

namespace Luna
{
    public class OnMouseDown: MonoBehaviour
    {
        public UltEvent<Vector3> OnMouseButtonDown;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseButtonDown.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}