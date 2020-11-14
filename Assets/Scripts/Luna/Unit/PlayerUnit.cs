using System.Collections.Generic;
using UnityEngine;

namespace Luna.Unit
{
    [RequireComponent(typeof(PlayerInputAction))]
    public class PlayerUnit : Unit
    {
        private PlayerInputAction _input;
        private List<IUnitAction> _actions;

        private void Awake()
        {
            _input = GetComponent<PlayerInputAction>();
        }

        public override List<IUnitAction> StartTurn()
        {
            _input.ResetAction();
            return new List<IUnitAction>(1) {_input};
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