using System;
using System.Collections.Generic;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;
using Util;

namespace Luna.Actions
{

    [RequireComponent(typeof(Pathfinding))]
    public class PlayerInputAction : ActionBehaviour
    {
        [SerializeField] private Weapon mainWeapon;

        private List<Grid.Grid.Node> _path;
        private bool _isCapturing;

        private Pathfinding _pathfinding;
        private Unit.Unit _unit;
        private MouseController _mouse;

        private bool _isFinished;

        private void Awake()
        {
            _pathfinding = GetComponent<Pathfinding>();
            _mouse = FindObjectOfType<MouseController>();
        }

        public void OnPositionSelected(Vector3 position)
        {
            if (_isCapturing)
            {
                var actionsQueued = QueueActions(position);

                if (actionsQueued)
                {
                    EndTurn();
                }
            }
            else
            {
                // cancel any ongoing paths
                _path = null;
            }
        }

        private bool QueueActions(Vector3 position)
        {
            Grid.Grid.Node clickedNode = new Grid.Grid.Node();
            var isClickedNodeValid = _unit.Occupant.Grid.TryGetNodeAtWorldPosition(position, ref clickedNode);


            Grid.Grid.Node myNode = new Grid.Grid.Node();
            var isMyNodeValid = _unit.Occupant.Grid.TryGetNodeAtWorldPosition(transform.position, ref myNode);

            if (isMyNodeValid && isClickedNodeValid)
            {
                // attack
                var direction = clickedNode.Position - _unit.Occupant.Occupant.Position;
                var targets = mainWeapon.FindTargets(_unit.Occupant.Occupant, direction,_unit.Occupant.Grid);

                if (Array.Exists(targets, it => it.Position == clickedNode.Position))
                {
                    var actions = mainWeapon.Use(_unit.Occupant.Occupant, direction, _unit.Occupant.Grid);

                    if (actions != null && actions.Count > 0)
                    {
                        _unit.QueueRange(actions);
                        return true;
                    }

                }


                // move
                _path = _pathfinding.CalculatePath(transform.position, position);

                if (_path != null && _path.Count > 0)
                {
                    var first = _path[0];

                    _path.RemoveAt(0);


                    _unit.QueueAction(new MoveToPointAction(_unit, first));
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            _isFinished = false;
        }

        public void EndTurn()
        {
            if (_isCapturing)
            {
                _mouse.SetIndicator(false);
                _isFinished = true;
                _isCapturing = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (_path == null || _unit == null) return;

            foreach (var node in _path)
            {
                var pos = _unit.Occupant.Grid.GetWorldPos(node);

                Gizmos.color = Color.blue;
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }

        public override void StartAction(Unit.Unit unit)
        {
            Reset();
            _unit = unit;
            if (_path != null && _path.Count > 0)
            {
                unit.QueueAction(new MoveToPointAction(_unit, _path[0]));
                _path.RemoveAt(0);
                _isFinished = true;
            }
            else
            {
                _isCapturing = true;
                _mouse.SetIndicator(true);
            }
        }

        public override bool Tick(Unit.Unit unit)
        {
            return _isFinished;
        }

        [SerializeField] private RangedWeapon bombChucker;


        public void ThrowBomb()
        {
            if (_isCapturing)
            {
                _unit.QueueRange(bombChucker.Use(_unit.Occupant.Occupant, Vector2Int.up, _unit.Occupant.Grid));
                EndTurn();
            }
        }
    }
}