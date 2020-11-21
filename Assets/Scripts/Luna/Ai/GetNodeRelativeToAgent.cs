using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class GetNodeRelativeToAgent : BtNode
    {
        [SerializeField] private BlackboardKey relativePosKey;
        [SerializeField] private BlackboardKey outputKey;

        protected override State OnExecute(AgentContext context)
        {
            if (!context.AgentBlackboard.Contains(relativePosKey)) return State.Failed;

            var relativePos = context.AgentBlackboard.RetrieveData<Vector2Int>(relativePosKey);
            var nodeIdx = context.Occupant.CurrentNodeIdx + relativePos;

            var node = new Grid.Grid.Node();
            if (context.Occupant.Grid.TryGetNodeAt(nodeIdx.x, nodeIdx.y, ref node))
            {
                context.AgentBlackboard.Add<Grid.Grid.Node?>(outputKey, node);
                return State.Succeeded;
            }
            else
            {
                return State.Failed;
            }
        }
    }
}