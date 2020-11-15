using System;
using System.Collections.Generic;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;
using Util;

namespace Luna.Actions
{
    // todo(chris): this many RequireComponents is probably a good sign that this has too many colaborators
    [RequireComponent(
        typeof(Pathfinding),
        typeof(Unit.Unit),
        typeof(IProvider<GridVariable>)
    )]
    [RequireComponent(typeof(GridOccupantBehaviour))]
    public class PlayerInputAction : MonoBehaviour, IUnitAction
    {
        [SerializeField] private MouseController mouse;
        [SerializeField] private Weapon mainWeapon;

        private List<Grid.Grid.Node> _path;
        private GridVariable _grid;
        private bool _isCapturing;

        private Pathfinding _pathfinding;
        private Unit.Unit _unit;
        private GridOccupantBehaviour _gridOccupant;

        private void Awake()
        {
            _pathfinding = GetComponent<Pathfinding>();
            _gridOccupant = GetComponent<GridOccupantBehaviour>();
            _grid = GetComponent<IProvider<GridVariable>>()?.Get();
            _unit = GetComponent<Unit.Unit>();

            if (_grid == null)
            {
                Debug.LogError("Grid required to do work");
            }
        }

        public void Execute()
        {
            IsStarted = true;
            IsFinished = false;

            if (_path != null && _path.Count > 0)
            {
                _unit.AddAction(new MoveToPointAction(_unit, _path[0]));
                _path.RemoveAt(0);
                IsFinished = true;
            }
            else
            {
                _isCapturing = true;
                mouse.SetIndicator(true);
            }
        }

        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public TurnPhase Phase => TurnPhase.ChoosingAction;

        public void OnPositionSelected(Vector3 position)
        {
            if (_isCapturing)
            {
                var action = BuildActions(position);

                if (action != null)
                {
                    _unit.AddActions(action);

                    EndTurn();
                }
            }
            else
            {
                // cancel any ongoing paths
                _path = null;
            }
        }

        private List<IUnitAction> BuildActions(Vector3 position)
        {
            Grid.Grid.Node clickedNode = new Grid.Grid.Node();
            var isClickedNodeValid = _grid.Value.TryGetNodeAtWorldPosition(position, ref clickedNode);


            Grid.Grid.Node myNode = new Grid.Grid.Node();
            var isMyNodeValid = _grid.Value.TryGetNodeAtWorldPosition(transform.position, ref myNode);

            if (isMyNodeValid && isClickedNodeValid)
            {
                // attack
                var targets = mainWeapon.FindTargets(myNode, _grid.Value);

                if (Array.Exists(targets, it => it.Equals(clickedNode)))
                {
                    return mainWeapon.Apply(clickedNode, gameObject);
                }


                // move
                _path = _pathfinding.CalculatePath(transform.position, position);

                if (_path != null && _path.Count > 0)
                {
                    var first = _path[0];

                    _path.RemoveAt(0);

                    return new List<IUnitAction>(1){new MoveToPointAction(_unit, first)};
                }
            }

            return null;
        }

        public void Reset()
        {
            IsFinished = false;
            IsStarted = false;
        }

        public void EndTurn()
        {
            if (_isCapturing)
            {
                mouse.SetIndicator(false);
                IsFinished = true;
                _isCapturing = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (_path == null || _grid == null || _grid.Value == null) return;

            foreach (var node in _path)
            {
                var pos = _grid.Value.GetWorldPos(node);

                Gizmos.color = Color.blue;
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}