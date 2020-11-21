using System;
using Ai;
using UnityEngine;
using Util;
using Util.Ai;

namespace Luna.Ai
{
    [RequireComponent(typeof(Agent))]
    public class ReadCurrentKeyBehaviour : MonoBehaviour
    {
        [SerializeField] private BlackboardKey key;
        public string Value;

        private Agent _agent;


        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Update()
        {
            Value = _agent.AgentBlackboard.Retrieve(key)?.Data?.ToString() ?? "Cant Find Key";
        }
    }
}