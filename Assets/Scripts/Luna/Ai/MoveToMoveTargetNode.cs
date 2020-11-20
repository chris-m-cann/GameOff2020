using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class MoveToMoveTargetNode : BtNode
    {
        public override State Execute(AgentContext context)
        {
            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();
            var moveTarget = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>("MoveTarget");
            var unit = context.AgentBlackboard.RetrieveData<Unit.Unit>("Unit");

            if (occupant == null || moveTarget == null || unit == null) return State.Failed;

            unit.AddAction(new MoveToPointAction(unit, moveTarget.Value));
            return State.Succeeded;
        }
    }
}