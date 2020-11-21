using Ai;
using Luna.Grid;
using Luna.Unit;
using UnityEngine;

namespace Util.Ai
{
    public struct AgentContext
    {
        public Blackboard GlobalBlackboard;
        public Blackboard AgentBlackboard;

        public GameObject Agent;
        public Transform Target;

        public bool LogNodeExecution;

        // game specific
        public Unit Unit;
        public GridOccupantBehaviour Occupant;
    }
}