using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Luna
{
    public class TurnController : MonoBehaviour
    {
        [Tooltip("an ordered list of groups of actors that take turns together, the order of the lists is the turn order")]
        [SerializeField] private RuntimeSet<TurnTaker>[] groups;

        private int _turnIndex = -1;

        public void BeingTurns()
        {
            _turnIndex = 0;
            foreach (var actor in groups[_turnIndex])
            {
                actor.StartTurn();
            }
        }
        
        private void Update()
        {
            if (_turnIndex < 0) return;

            if (AllFinished())
            {
                MoveToNextGroup();
                StartNextTurn();
            }
        }

        private void StartNextTurn()
        {
            foreach (var actor in groups[_turnIndex])
            {
                actor.StartTurn();
            }
        }

        private bool AllFinished()
        {
            foreach (var actor in groups[_turnIndex])
            {
                if (!actor.IsTurnFinished()) return false;
            }

            return true;
        }

        private void MoveToNextGroup()
        {
            _turnIndex++;
            if (_turnIndex >= groups.Length)
            {
                _turnIndex = 0;
            }
        }
    }
}