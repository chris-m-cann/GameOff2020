using UnityEngine;
using Util.Ai;

namespace Luna.Actions
{
    public class ExecuteAgentAction : BaseAction
    {
        private Agent _agent;
        public ExecuteAgentAction(Unit.Unit unit, Agent agent) : base(unit)
        {
            _agent = agent;
        }

        public override void Execute()
        {
            IsStarted = true;
            _agent.Execute();
            IsFinished = true;
        }

        public override TurnPhase Phase { get => TurnPhase.ChoosingAction; }
    }
}