using System;
using System.Collections;
using System.Collections.Generic;
using Luna.Actions;
using Luna.Grid;
using UltEvents;
using UnityEngine;
using Util;

namespace Luna
{
    [RequireComponent(typeof(GridOccupantBehaviour), typeof(Unit.Unit))]
    public class MoveAlongPath : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private UltEvent onStartMove;
        [SerializeField] private UltEvent OnStopMove;
        [SerializeField] private bool moveToLastNodeOnCollision = true;

        private Collider2D _collider2D;

        private GridOccupantBehaviour _occupant;
        private Unit.Unit _unit;
        private OnCollisionBehaviour _collisionBehaviour;
        private Collider2D _myCollider;

        private void Awake()
        {
            _myCollider = GetComponent<Collider2D>();
            _collisionBehaviour = GetComponent<OnCollisionBehaviour>();
            _unit = GetComponent<Unit.Unit>();
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public void Move(Grid.Grid.Node destination, Action onComplete, bool twoWayCollisions = false)
        {
            StartCoroutine(CoMove(destination, speed, twoWayCollisions, onComplete));
        }

        private IEnumerator CoMove(Grid.Grid.Node destination, float speed, bool twoWayCollisions, Action onComplete)
        {
            yield return StartCoroutine(CoMove(destination, speed, twoWayCollisions));
            onComplete();
        }

        private IEnumerator CoMove(Grid.Grid.Node destination, float speed, bool twoWayCollisions)
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

            onStartMove.Invoke();

            while (Time.time < end && _collider2D == null)
            {
                var t = (Time.time - start) / duration;
                var x = Tween.Lerp(startPoint.x, endPoint.x, t);
                var y = Tween.Lerp(startPoint.y, endPoint.y, t);
                transform.position = new Vector3(x, y, transform.position.z);

                if (moveToLastNodeOnCollision)
                {
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
                }

                yield return null;
            }

            OnStopMove.Invoke();

            var colliders = new Collider2D[1];
            if (_collider2D == null && _myCollider != null)
            {
                if (Physics2D.OverlapCollider(_myCollider, new ContactFilter2D(), colliders) > 0)
                {
                    _collider2D = colliders[0];
                }
            }

            if (_collider2D == null)
            {
                transform.position = new Vector3(endPoint.x, endPoint.y, transform.position.z);
            }
            else
            {
                OnCollision(startNode.Value, lastNode, speed, twoWayCollisions);
            }
        }

        private void OnCollision(Grid.Grid.Node startNode, Grid.Grid.Node lastNode, float speed, bool twoWayCollisions)
        {
            if (_collisionBehaviour != null)
            {
                var colliderOccupant = _collider2D.gameObject.GetComponent<GridOccupantBehaviour>();
                _unit.AddActions(_collisionBehaviour.CollideWith(colliderOccupant.Occupant, colliderOccupant.CurrentNode.Value));
            }

            if (twoWayCollisions)
            {
                var collision = _collider2D.GetComponent<OnCollisionBehaviour>();
                if (collision != null)
                {
                    _unit.AddActions(collision.CollideWith(_occupant.Occupant, startNode));
                }
            }
            _unit.AddAction(new MoveToPointAction(_unit, lastNode, false, TurnPhase.ResolvingMoveCollisions));
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            _collider2D = other;
        }
    }
}