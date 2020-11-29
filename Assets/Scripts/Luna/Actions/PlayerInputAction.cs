using System;
using System.Collections.Generic;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using TMPro.EditorUtilities;
using UnityEngine;
using Util;
using Util.Inventory;

namespace Luna.Actions
{

    [RequireComponent(typeof(Pathfinding), typeof(IProvider<Inventory>))]
    public class PlayerInputAction : ActionBehaviour
    {
        [SerializeField] private MeleeWeapon mainWeapon;
        [SerializeField] private InventoryKey bombKey;


        private bool _isCapturing;
        private bool _isUsingBomb;
        private bool _isUsingItem;
        private bool _isFinished;

        private Pathfinding _pathfinding;
        private Unit.Unit _unit;
        private MouseController _mouse;
        private Inventory _inventory;

        private void Awake()
        {
            _pathfinding = GetComponent<Pathfinding>();
            _mouse = FindObjectOfType<MouseController>();
            _inventory = GetComponent<IProvider<Inventory>>()?.Get();
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
        }

        private bool QueueActions(Vector3 position)
        {
            Grid.Grid.Node clickedNode = new Grid.Grid.Node();
            var isClickedNodeValid = _unit.Occupant.Grid.TryGetNodeAtWorldPosition(position, ref clickedNode);


            Grid.Grid.Node myNode = new Grid.Grid.Node();
            var isMyNodeValid = _unit.Occupant.Grid.TryGetNodeAtWorldPosition(transform.position, ref myNode);

            if (isMyNodeValid && isClickedNodeValid)
            {

                if (_isUsingBomb)
                {
                    if (_inventory == null) return false;

                    var diff = clickedNode.Position - myNode.Position;
                    if (!diff.IsCardinal()) return false;

                    AggregateSlot slot;
                    if (!_inventory.RetrieveSlot(bombKey, out slot)) return false;

                    slot.Total -= 1;

                    _inventory.UpdateSlot(bombKey, slot);

                    _unit.QueueRange(bombChucker.Use(_unit.Occupant.Occupant, diff, _unit.Occupant.Grid));

                    return true;
                }
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
                var path = _pathfinding.CalculatePath(transform.position, position);

                if (path != null && path.Count > 0)
                {
                    var first = path[0];

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
                _mouse.SetIndicator(false, _unit.Occupant, mainWeapon);
                _isFinished = true;
                _isCapturing = false;
                _isUsingBomb = false;
            }
        }


        public override void StartAction(Unit.Unit unit)
        {
            Reset();
            _unit = unit;

            _isCapturing = true;
            _mouse.SetIndicator(true, _unit.Occupant, mainWeapon);
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
                _isUsingBomb = !_isUsingBomb;

                if (_isUsingBomb)
                {
                    _mouse.EnterRangedMode(bombChucker);
                }
                else
                {
                    _mouse.EnterDefaultMode(mainWeapon as MeleeWeapon);
                }
            }
        }
    }
}