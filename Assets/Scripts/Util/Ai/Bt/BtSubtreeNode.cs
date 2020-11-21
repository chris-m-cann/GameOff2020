using UnityEngine;

namespace Util.Ai.Bt
{
    public class BtSubtreeNode : BtNode
    {
        [SerializeField] private BehaviourTree tree;

        protected override State OnExecute(AgentContext context)
        {
            return tree?.Root?.Execute(context) ?? State.Failed;
        }
    }
}