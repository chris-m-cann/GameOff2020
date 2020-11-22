using UnityEngine;

namespace Util.Ai.Bt
{
    public class IsKeyInBlackboardNode : BtNode
    {
        [SerializeField] private BlackboardKey key;

        protected override State OnExecute(AgentContext context)
        {
            return context.AgentBlackboard.Contains(key) ? State.Succeeded : State.Failed;
        }
    }
}