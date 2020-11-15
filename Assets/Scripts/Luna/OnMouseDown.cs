using System;
using UltEvents;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseButtonDown.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}