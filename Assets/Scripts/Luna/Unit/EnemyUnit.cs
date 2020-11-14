using System.Collections.Generic;
using Luna.Grid;
using UnityEngine;

namespace Luna.Unit
{
    [RequireComponent(typeof(GridOccupantBehaviour))]
    public class EnemyUnit : Unit
    {
        private List<IUnitAction> _actions;

        private bool _upNext;

        private GridOccupantBehaviour _occupant;

        private void Awake()
        {
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public override List<IUnitAction> StartTurn()
        {
            var nexPos = _upNext ? Vector2Int.up : Vector2Int.down;
            _upNext = !_upNext;

            nexPos = _occupant.CurrentNodeIdx + nexPos;

            Grid.Grid.Node nextNode = new Grid.Grid.Node();

            if (_occupant.Get().Value.TryGetNodeAt(nexPos.x, nexPos.y, ref nextNode))
            {
                return new List<IUnitAction>(1){new MoveToPointAction(this, nextNode)};
            }

            return null;
        }

        public override List<IUnitAction> Tick()
        {
            var tmp = _actions;
            _actions = null;
            return tmp;
        }

        public override void AddAction(IUnitAction action)
        {
            if (_actions == null)
            {
                _actions = new List<IUnitAction>();
            }

            _actions.Add(action);
        }

        public override void AddActions(List<IUnitAction> actions)
        {
            if (actions == null || actions.Count == 0) return;

            if (_actions == null)
            {
                _actions = actions;
            }
            else
            {
                _actions.AddRange(actions);
            }
        }
    }
}