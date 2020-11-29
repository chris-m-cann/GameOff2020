using System;
using System.Collections;
using UnityEngine;

namespace Luna.Actions
{
    public class RunActionAction : IUnitAction
    {
        private readonly Action _action;
        private readonly int _priority;
        private readonly GameObject _target;

        private bool _isFinished;

        public RunActionAction(GameObject target, Action action, int priority = 0)
        {
            _action = action;
            _priority = priority;
            _target = target;
        }

        private IEnumerator Execute()
        {
            if (_target != null)
            {
                _action.Invoke();
            }

            yield return null;
            _isFinished = true;
        }
        public void StartAction(Unit.Unit unit)
        {
            unit.StartCoroutine(Execute());
        }

        public bool Tick(Unit.Unit actor)
        {
            return _isFinished;
        }

        public int Priority => _priority;
    }
}