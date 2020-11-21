using Luna.Grid;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class OnCardinalDirectionNode : BtNode
    {
        [SerializeField] private BlackboardKey targetNodeKey;
        protected override State OnExecute(AgentContext context)
        {
            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();
            var target = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(targetNodeKey);

            if (occupant == null || target == null) return State.Failed;

            var agentNode = occupant.CurrentNode;

            if (agentNode == null) return State.Failed;

            if (agentNode.Value.X != target.Value.X && agentNode.Value.Y != target.Value.Y)
            {
                return State.Failed;
            }
            else
            {
                return State.Succeeded;
            }
        }
    }
}