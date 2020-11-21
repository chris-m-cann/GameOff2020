using Luna.Grid;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class DirectionToTargetNode : BtNode
    {
        [SerializeField] private BlackboardKey targetNodeKey;
        [SerializeField] private BlackboardKey outputKey;
        protected override State OnExecute(AgentContext context)
        {
            var targetNode = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(targetNodeKey);

            if (targetNode == null) return State.Failed;

            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();

            if (occupant == null) return State.Failed;

            var agentNode = occupant.CurrentNode;

            if (agentNode == null) return State.Failed;

            var dir = new Vector2Int(targetNode.Value.X - agentNode.Value.X, targetNode.Value.Y - agentNode.Value.Y);

            context.AgentBlackboard.Add(outputKey, dir);

            return State.Succeeded;
        }
    }
}