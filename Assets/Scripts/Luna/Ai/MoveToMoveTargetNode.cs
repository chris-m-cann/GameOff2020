using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class MoveToMoveTargetNode : BtNode
    {
        [SerializeField] private BlackboardKey moveTargetKey;


        protected override State OnExecute(AgentContext context)
        {
            var moveTarget = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(moveTargetKey);
            if (moveTarget == null) return State.Failed;

            context.Unit.QueueAction(new MoveToPointAction(context.Unit, moveTarget.Value));
            return State.Succeeded;
        }
    }
}