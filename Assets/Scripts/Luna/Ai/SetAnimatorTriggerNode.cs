using UnityEngine;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class SetAnimatorTriggerNode : BtNode
    {
        [SerializeField] private string trigger;

        protected override State OnExecute(AgentContext context)
        {
            var animation = context.Agent.GetComponent<Animator>();

            if (animation == null) return State.Failed;

            animation.SetTrigger(trigger);

            return State.Succeeded;
        }
    }
}