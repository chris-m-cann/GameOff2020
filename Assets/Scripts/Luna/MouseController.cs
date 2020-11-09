using System;
using System.Collections.Generic;
using UnityEngine;

namespace Luna
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private Transform worldMouse;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (worldMouse == null && worldMouse.gameObject.activeSelf) return;
            var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            var newPos = new Vector3(Mathf.FloorToInt(worldPoint.x) +.5f, Mathf.FloorToInt(worldPoint.y) + .5f, worldMouse.position.z);
            worldMouse.position = newPos;
        }

        public void SetIndicator(bool b)
        {
            worldMouse.gameObject.SetActive(b);
        }
    }
}