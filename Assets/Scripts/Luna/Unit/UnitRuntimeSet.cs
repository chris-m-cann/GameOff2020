using System;
using System.Collections;
using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;
using Util;

namespace Luna.Unit
{
    [CreateAssetMenu(menuName = "Custom/RuntimeSets/Unit")]
    public class UnitRuntimeSet : RuntimeSet<Unit>
    {
        [NonSerialized]
        private int _idx = 0;
        [NonSerialized]
        private bool _currentUnitRemoved = false;

        private Unit Current => items[_idx];

        public override bool Remove(Unit thing)
        {
            var thingIdx = items.IndexOf(thing);
            return RemoveAt(thingIdx);
        }

        public override bool RemoveAt(int idx)
        {
            if (base.RemoveAt(idx))
            {
                if (idx < _idx)
                {
                    --_idx;
                } else if (idx == _idx)
                {
                    _currentUnitRemoved = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool MoveNext()
        {
            if (IsEmpty) return false;

            _currentUnitRemoved = false;
            _idx = (_idx + 1) % items.Count;

            return _idx != 0;
        }

        public void Start()
        {
            if (!IsEmpty)
            {
                _idx = 0;
                _currentUnitRemoved = false;

                Current.StartTurn();
            }
        }

        public bool RunCurrentUnit()
        {
            if (_floatingActions.Count > 0)
            {
                if (_floatingActions.Peek().Tick(null))
                {
                    _floatingActions.Dequeue();

                    if (_floatingActions.Count > 0)
                    {
                        _floatingActions.Peek().StartAction(null);
                    }

                }

                return false;
            }

            if (IsEmpty || _currentUnitRemoved || Current.Tick())
            {
                if (MoveNext())
                {
                    Current.StartTurn();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override void Reset(ResetScenario scenario = ResetScenario.OnDemand)
        {
            base.Reset(scenario);
            _idx = 0;
            _currentUnitRemoved = false;
        }

        // todo(chris) need to handle the case where i am adding actions to a dead unit or when none left in the set
        public void AddActionsToCurrentUnit(IEnumerable<IUnitAction> actions)
        {
            if (IsEmpty || _currentUnitRemoved)
            {
                if (actions != null)
                {
                    foreach (var action in actions)
                    {
                        _floatingActions.Enqueue(action, action.Priority);
                    }

                    _floatingActions.Peek().StartAction(null);
                }
            }
            else
            {
                Current.QueueRange(actions);
            }
        }

        private readonly PriorityQueue<IUnitAction, int> _floatingActions = new PriorityQueue<IUnitAction, int>();
    }
}