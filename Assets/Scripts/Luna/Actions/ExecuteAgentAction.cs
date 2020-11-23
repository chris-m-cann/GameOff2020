using System;
using UnityEngine;
using Util.Ai;

namespace Luna.Actions
{
    [RequireComponent(typeof(Agent))]
    public class ExecuteAgentAction : ActionBehaviour
    {
        private Agent _agent;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        public override void StartAction(Unit.Unit unit)
        {
            _agent.Execute();
        }

        public override bool Tick(Unit.Unit actor) => true;
    }
}