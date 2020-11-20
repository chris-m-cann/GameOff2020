using UnityEngine;

namespace Util.Ai.Bt
{
    public class IsTargetInRangeBtNode : BtNode
    {
        [SerializeField] private float range;

        public IsTargetInRangeBtNode()
        {

        }
        public IsTargetInRangeBtNode(float range)
        {
            this.range = range;
        }

        public override State Execute(AgentContext context)
        {
            var target = context.Target;
            if (target == null) return State.Failed;

            if (Vector3.Distance(target.position, context.Agent.transform.position) <= range)
            {
                return State.Succeeded;
            }
            else
            {
                return State.Failed;
            }
        }
    }
}