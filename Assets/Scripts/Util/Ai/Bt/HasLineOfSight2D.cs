using Ai;
using UnityEngine;

namespace Util.Ai.Bt
{
    public class HasLineOfSight2D : BtNode
    {
        [SerializeField] private LayerMask layers;
        [SerializeField] private float range;

        protected override State OnExecute(AgentContext context)
        {
            var target = context.Target;

            if (target == null) return State.Failed;
            var hit = Physics2D.Raycast(context.Agent.transform.position, target.position - context.Agent.transform.position, range, layers);

            if (hit.collider?.transform == target)
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