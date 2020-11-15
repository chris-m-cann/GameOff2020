using System.Collections.Generic;
using Luna.Grid;
using UnityEngine;

namespace Luna.Unit
{

    [RequireComponent(typeof(GridOccupantBehaviour))]
    public class BaseUnit : Unit
    {
        protected List<IUnitAction> _actions;

        public override List<IUnitAction> StartTurn() => null;

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