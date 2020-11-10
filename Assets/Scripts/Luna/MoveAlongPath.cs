using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Luna
{
    public class MoveAlongPath : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        public void Move(List<Grid.Grid.Node> path, Action onComplete)
        {
            StartCoroutine(CoMove(path, onComplete));
        }

        public void Move(Grid.Grid.Node destination, Action onComplete)
        {
            StartCoroutine(CoMove(destination.WorldPosition, onComplete));
        }

        public void Move(Vector3 destination, Action onComplete)
        {
            StartCoroutine(CoMove(destination, onComplete));
        }

        private IEnumerator CoMove(List<Grid.Grid.Node> path, Action onComplete)
        {
            foreach (var node in path)
            {
                yield return StartCoroutine(CoMove(node.WorldPosition, speed));
            }

            onComplete();
        }

        private IEnumerator CoMove(Vector3 destination, Action onComplete)
        {
            yield return StartCoroutine(CoMove(destination, speed));
            onComplete();
        }

        private IEnumerator CoMove(Vector2 endPoint, float speed)
        {
            var startPoint = (Vector2) transform.position;
            var start = Time.time;
            var distance = (endPoint - startPoint).magnitude;
            var duration = distance / speed;
            var end = start + duration;

            while (Time.time < end)
            {
                var t = (Time.time - start) / duration;
                var x = Tween.Lerp(startPoint.x, endPoint.x, t);
                var y = Tween.Lerp(startPoint.y, endPoint.y, t);
                transform.position = new Vector3(x, y, transform.position.z);

                yield return null;
            }

            transform.position = new Vector3(endPoint.x, endPoint.y, transform.position.z);

        }
    }
}