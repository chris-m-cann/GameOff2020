using UnityEngine;

namespace Util.Ai.Bt
{
    public class WriteToAgentBlackboardNode : BtDecoratorNode
    {
        [SerializeField] private BlackboardKey key;
        [SerializeField] private string value;


        protected override State OnExecute(AgentContext context)
        {
            context.AgentBlackboard.Add(key, value);

            return child.Execute(context);
        }
    }
}