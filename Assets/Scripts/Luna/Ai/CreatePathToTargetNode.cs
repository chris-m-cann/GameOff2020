using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class CreatePathToTargetNode : BtNode
    {
        [SerializeField] private BlackboardKey outputKey;

        protected override State OnExecute(AgentContext context)
        {
            var pathfinding = context.Agent.GetComponent<Pathfinding>();
            if (pathfinding == null) return State.Failed;

            if (context.Target == null) return State.Failed;

            var path = pathfinding.CalculatePath(context.Agent.transform.position, context.Target.position, false);

            if ((path?.Count ?? 0) < 1) return State.Failed;

            context.AgentBlackboard.Add<Grid.Grid.Node?>(outputKey, path[0]);

            return State.Succeeded;
        }
    }
}