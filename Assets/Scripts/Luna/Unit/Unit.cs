using System.Collections.Generic;
using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;

namespace Luna.Unit
{
    [RequireComponent(typeof(GridOccupantBehaviour))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private ActionBehaviour[] startUpActions;

        private readonly PriorityQueue<IUnitAction, int> _actions = new PriorityQueue<IUnitAction, int>();

        [HideInInspector]
        public GridOccupantBehaviour Occupant;

        private void Awake()
        {
            Occupant = GetComponent<GridOccupantBehaviour>();
        }

        public void StartTurn()
        {
            foreach (var action in startUpActions)
            {
                _actions.Enqueue(action, action.Priority);
            }

            if (_actions.Count != 0)
            {
                _actions.Peek().StartAction(this);
            }
        }

        public bool Tick()
        {
            if (_actions.Count == 0) return true;

            var actionDone = _actions.Peek().Tick(this);
            if (!actionDone) return false;


            _actions.Dequeue();
            if (_actions.Count == 0) return true;

            _actions.Peek().StartAction(this);
            return false;
        }

        public void QueueAction(IUnitAction action) => _actions.Enqueue(action, action.Priority);

        public void QueueRange(IEnumerable<IUnitAction> actions)
        {
            if (actions == null) return;
            foreach (var action in actions)
            {
                QueueAction(action);
            }
        }
    }
}