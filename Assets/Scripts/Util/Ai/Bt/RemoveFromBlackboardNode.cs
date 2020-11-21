using UnityEngine;

namespace Util.Ai.Bt
{
    public class RemoveFromBlackboardNode : BtNode
    {
        [SerializeField] private BlackboardKey key;

        protected override State OnExecute(AgentContext context)
        {
            context.AgentBlackboard.Remove(key);

            return State.Succeeded;
        }
    }
}