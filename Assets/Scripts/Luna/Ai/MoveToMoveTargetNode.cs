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
            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();
            var moveTarget = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(moveTargetKey);
            var unit = context.Agent.GetComponent<Unit.Unit>();

            if (occupant == null || moveTarget == null || unit == null) return State.Failed;

            unit.AddAction(new MoveToPointAction(unit, moveTarget.Value));
            return State.Succeeded;
        }
    }
}