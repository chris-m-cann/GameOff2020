using System;
using System.Collections.Generic;
using Ai;
using Luna.Actions;
using UnityEngine;
using Util.Ai;

namespace Luna.Unit
{
    [RequireComponent(typeof(Agent))]
    public class AiUnit : BaseUnit
    {
        public Transform player;
        private Agent _agent;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Start()
        {
            _agent.AgentBlackboard.Add(Blackboard.StringToKey("Unit"), this);
            _agent.Target = player;
        }

        public override List<IUnitAction> StartTurn()
        {
            return new List<IUnitAction>{new ExecuteAgentAction(this, _agent)};
        }
    }
}