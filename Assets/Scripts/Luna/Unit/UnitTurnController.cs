using System;
using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using UnityEditor;
using UnityEngine;
using Util;

namespace Luna.Unit
{
    public class UnitTurnController : MonoBehaviour
    {
        [Tooltip(
            "an ordered list of groups of actors that take turns together, the order of the lists is the turn order")]
        [SerializeField]
        private UnitRuntimeSet[] groups;

        private int _currentGroup = -1;


        public void BeingTurns()
        {
            StartNextTurn();
        }

        private void StartNextTurn()
        {
            _currentGroup = (_currentGroup + 1) % groups.Length;
            var initial = _currentGroup;
            while (groups[_currentGroup].IsEmpty)
            {
                _currentGroup = (_currentGroup + 1) % groups.Length;

                if (_currentGroup == initial)
                {
                    throw new Exception("Critical issue, no units in runtime sets!!");
                }
            }

            groups[_currentGroup].Start();
        }


        private void Update()
        {
            if (_currentGroup == -1) return; // sentinal value to say we havnt started

            if (!groups[_currentGroup].RunCurrentUnit())
            {
                StartNextTurn();
            }
        }

        public void AddActionsToCurrentUnit(IEnumerable<IUnitAction> actions)
        {
            groups[_currentGroup].AddActionsToCurrentUnit(actions);
        }
    }
}