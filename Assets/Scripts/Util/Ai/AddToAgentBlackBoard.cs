using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Util.Ai
{
    [RequireComponent(typeof(Agent))]
    public class AddToAgentBlackBoard : MonoBehaviour
    {
        [SerializeField] private Pair<BlackboardKey, BlackboardTypes>[] entries;
        private Agent _agent;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Start()
        {
            foreach (var entry in entries)
            {
                AddToBlackboard(entry);
            }
        }

        public void AddToBlackboard(Pair<BlackboardKey, BlackboardTypes> entry)
        {
           AddToBlackboard(entry.First, entry.Second);
        }

        public void AddToBlackboard(BlackboardKey key, BlackboardTypes entry)
        {
            entry.AddToBlackboard(_agent.AgentBlackboard, key);
        }


        public void RemoveFromBlackboard(BlackboardKey key)
        {

        }
    }
}