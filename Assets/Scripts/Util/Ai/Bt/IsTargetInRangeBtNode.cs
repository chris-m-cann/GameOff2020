using UnityEngine;

namespace Util.Ai.Bt
{
    public class IsTargetInRangeBtNode : BtNode
    {
        private float _range;

        public IsTargetInRangeBtNode(float range)
        {
            _range = range;
        }

        public override State Execute(AgentContext context)
        {
            var target = context.Target;
            if (target == null) return State.Failed;

            if (Vector3.Distance(target.position, context.Agent.transform.position) <= _range)
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