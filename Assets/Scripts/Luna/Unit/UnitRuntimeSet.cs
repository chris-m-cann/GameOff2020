using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}