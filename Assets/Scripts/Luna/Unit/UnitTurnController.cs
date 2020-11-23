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
        private int _currentUnit = -1;


        public void BeingTurns()
        {
            _currentGroup = 0;
            _currentUnit = -1;

            StartNextTurn();
        }

        private void StartNextTurn()
        {
            _currentUnit++;

            while (!(_currentGroup < groups.Length && _currentUnit < groups[_currentGroup].ListView.Count))
            {
                if (groups[_currentGroup].ListView.Count <= _currentUnit)
                {
                    _currentUnit = 0;
                    _currentGroup++;

                    if (groups.Length <= _currentGroup)
                    {
                        _currentGroup = 0;
                    }
                }
            }

            groups[_currentGroup].ListView[_currentUnit].StartTurn();
        }


        private void Update()
        {
            if (_currentGroup == -1) return; // sentinal value to say we havnt started

            if (groups[_currentGroup].ListView[_currentUnit].Tick()) StartNextTurn();
        }
    }
}