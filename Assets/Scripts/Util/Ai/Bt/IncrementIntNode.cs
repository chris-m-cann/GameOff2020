using UnityEngine;

namespace Util.Ai.Bt
{
    public class IncrementIntNode : BtNode
    {
        [SerializeField] private BlackboardKey intKey;
        [SerializeField] private int amount;


        protected override State OnExecute(AgentContext context)
        {
            if (context.AgentBlackboard.Contains(intKey))
            {
                var finalAmount = context.AgentBlackboard.RetrieveData<int>(intKey) + amount;
                context.AgentBlackboard.Add(intKey, finalAmount);
                return State.Succeeded;
            }

            return State.Failed;
        }
    }
}