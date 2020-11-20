using Luna.Grid;
using UnityEngine;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class CreateMoveTargetNode : BtNode
    {
        public override State Execute(AgentContext context)
        {
            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();
            if (occupant == null) return State.Failed;

            Vector2Int newPos = Vector2Int.zero;
            var node = new Grid.Grid.Node();
            bool noPointFound = true;
            while (noPointFound)
            {
                var direction = Random.Range(0, 2) == 1 ? Vector2Int.right : Vector2Int.up;
                var magnitude = Random.Range(0, 2) == 1 ? 1 : -1;

                newPos = occupant.CurrentNodeIdx + (magnitude * direction);

                if (occupant.Get().Value.TryGetNodeAt(newPos.x, newPos.y, ref node))
                {
                    noPointFound = node.Cost < 0;
                }
            }
            context.AgentBlackboard.Add<Grid.Grid.Node?>("MoveTarget", node);
            return State.Succeeded;
        }
    }
}