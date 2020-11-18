using Ai;
using UnityEngine;

namespace Util.Ai
{
    public struct AgentContext
    {
        public Blackboard GlobalBlackboard;
        public Blackboard AgentBlackboard;

        public GameObject Agent;
        public Transform Target;
    }
}