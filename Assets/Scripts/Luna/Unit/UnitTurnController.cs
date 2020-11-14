using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Util;

namespace Luna.Unit
{
    public class UnitTurnController : MonoBehaviour
    {
        [Tooltip("an ordered list of groups of actors that take turns together, the order of the lists is the turn order")]
        [SerializeField] private UnitRuntimeSet[] groups;

        [SerializeField] private TurnPhaseOrder phaseOrder;


        private Dictionary<TurnPhase, List<IUnitAction>> _actions = new Dictionary<TurnPhase, List<IUnitAction>>();

        private int _currentTurn = -1;
        private TurnPhase _currentPhase = TurnPhase.ChoosingAction;


        public void BeingTurns()
        {
            StartNextTurn();
        }

        private void AddAction(IUnitAction action)
        {
            if (!_actions.ContainsKey(action.Phase))
            {
                var list = new List<IUnitAction> {action};
                _actions[action.Phase] = list;

                return;
            }

            if (_actions[action.Phase] == null)
            {
                var list = new List<IUnitAction> {action};
                _actions[action.Phase] = list;

                return;
            }

            _actions[action.Phase].Add(action);
        }

        private void AddActions(List<IUnitAction> actions)
        {
            if (actions == null) return;

            foreach (var action in actions)
            {
                AddAction(action);
            }
        }

        private void Update()
        {
            if (_currentTurn < 0) return; // not started yet


            foreach (var unit in groups[_currentTurn])
            {
                var acts = unit.Tick();
                AddActions(acts);
            }

            var phaseActions = _actions.GetValueOrNull(_currentPhase);
            var allFinished = true;
            if (phaseActions != null)
            {
                for (int i = phaseActions.Count - 1; i > -1; i--)
                {
                    var action = phaseActions[i];
                    if (!action.IsStarted)
                    {
                        action.Execute();
                    }

                    if (action.IsFinished)
                    {
                        phaseActions.RemoveAt(i);
                    }
                }

                allFinished = phaseActions.Count == 0;
            }

            if (allFinished)
            {
                var next = phaseOrder.NextPhase(_currentPhase);
                _currentPhase = next.First;

                if (next.Second) // if has looped around then its the next turn
                {
                    StartNextTurn();
                }
            }
        }

        private void StartNextTurn()
        {
            MoveToNextGroup();
            foreach (var actor in groups[_currentTurn])
            {
                AddActions(actor.StartTurn());
            }
        }
        private void MoveToNextGroup()
        {
            _currentTurn++;
            if (_currentTurn >= groups.Length)
            {
                _currentTurn = 0;
            }
        }
    }
}