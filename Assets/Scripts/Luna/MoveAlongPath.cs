using System;
using System.Collections;
using System.Collections.Generic;
using Luna.Grid;
using UnityEngine;
using Util;

namespace Luna
{
    [RequireComponent(typeof(GridOccupantBehaviour), typeof(Unit.Unit))]
    public class MoveAlongPath : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private Collider2D _collider2D;

        private GridOccupantBehaviour _occupant;
        private Unit.Unit _unit;
        private OnCollisionBehaviour _collisionBehaviour;

        private void Awake()
        {
            _collisionBehaviour = GetComponent<OnCollisionBehaviour>();
            _unit = GetComponent<Unit.Unit>();
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public void Move(Grid.Grid.Node destination, Action onComplete)
        {
            StartCoroutine(CoMove(destination, speed, onComplete));
        }

        private IEnumerator CoMove(Grid.Grid.Node destination, float speed, Action onComplete)
        {
            yield return StartCoroutine(CoMove(destination, speed));
            onComplete();
        }

        private IEnumerator CoMove(Grid.Grid.Node destination, float speed)
        {
            Vector2 endPoint = destination.WorldPosition;

            var startNode = _occupant.CurrentNode;
            if (startNode == null) yield break;

            var lastNode = startNode.Value;

            var startPoint = (Vector2) transform.position;
            var start = Time.time;
            var distance = (endPoint - startPoint).magnitude;
            var duration = distance / speed;
            var end = start + duration;

            _collider2D = null;

            while (Time.time < end && _collider2D == null)
            {
                var t = (Time.time - start) / duration;
                var x = Tween.Lerp(startPoint.x, endPoint.x, t);
                var y = Tween.Lerp(startPoint.y, endPoint.y, t);
                transform.position = new Vector3(x, y, transform.position.z);

                var node = new Grid.Grid.Node();
                if (!_occupant.Get().Value.TryGetNodeAtWorldPosition(transform.position, ref node))
                {
                    transform.position = lastNode.WorldPosition;
                    yield break;
                }

                if (!node.Equals(lastNode))
                {
                    if (Vector2.Distance(transform.position, node.WorldPosition) < .1f)
                    {
                        lastNode = node;
                    }
                }

                yield return null;
            }

            if (_collider2D == null)
            {
                transform.position = new Vector3(endPoint.x, endPoint.y, transform.position.z);
            }
            else
            {
                yield return StartCoroutine(OnCollision(lastNode, speed));
            }
        }

        private IEnumerator OnCollision(Grid.Grid.Node lastNode, float speed)
        {
            if (_collisionBehaviour != null)
            {
                var colliderOccupant = _collider2D.gameObject.GetComponent<GridOccupantBehaviour>();
                _unit.AddActions(_collisionBehaviour.CollideWith(colliderOccupant.Occupant, colliderOccupant.CurrentNode.Value));
            }
            yield return CoMove(lastNode, speed);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            _collider2D = other;
        }
    }
}