using UnityEngine;

namespace Util.Ai.Bt
{
    public class BtSubtreeNode : BtNode
    {
        [SerializeField] private BehaviourTree tree;

        public override State Execute(AgentContext context)
        {
            return tree?.Root?.Execute(context) ?? State.Failed;
        }
    }
}