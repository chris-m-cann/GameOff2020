using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Util.Ai
{
    [RequireComponent(typeof(Agent))]
    public class AddToAgentBlackBoard : MonoBehaviour
    {
        [SerializeField] private Pair<BlackboardKey, Object>[] entries;
        private Agent _agent;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Start()
        {
            foreach (var entry in entries)
            {
                _agent.AgentBlackboard.Add(entry.First, entry.Second);
            }
        }
    }
}