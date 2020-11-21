using Luna.Grid;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class CreateMoveTargetFromDirNode : BtNode
    {
        [SerializeField] private BlackboardKey targetDirKey;
        [SerializeField] private BlackboardKey outputKey;

        [SerializeField] private int speed = 2;
        protected override State OnExecute(AgentContext context)
        {
            if (!context.AgentBlackboard.Contains(targetDirKey)) return State.Failed;
            var dir = context.AgentBlackboard.RetrieveData<Vector2Int>(targetDirKey);

            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();

            if (occupant == null) return State.Failed;

            var agentNode = occupant.CurrentNode;

            if (agentNode == null) return State.Failed;

            dir.x = MathUtils.ClampInt(dir.x, -1, 1);
            dir.y = MathUtils.ClampInt(dir.y, -1, 1);

            dir *= speed;
            dir.x += agentNode.Value.X;
            dir.y += agentNode.Value.Y;

            var moveNode = new Grid.Grid.Node();
            if (occupant.Get().Value.TryGetNodeAt(dir.x, dir.y, ref moveNode))
            {
                context.AgentBlackboard.Add<Grid.Grid.Node?>(outputKey, moveNode);
                return State.Succeeded;
            }
            else
            {
                return State.Failed;
            }
        }
    }
}