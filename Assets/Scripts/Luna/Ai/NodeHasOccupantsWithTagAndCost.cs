using System;
using System.Linq;
using Luna.Grid;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class NodeHasOccupantsWithTagAndCost : BtNode
    {
        [SerializeField] private GridOccupantType[] types;
        [SerializeField] private int cost;
        [SerializeField] private BlackboardKey nodeKey;

        protected override State OnExecute(AgentContext context)
        {
            var node = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(nodeKey);
            if (node == null) return State.Failed;

            var hasMatchingOccupant = node.Value.Occupants.Any(it => types.Contains(it.Type) && it.Cost == cost);

            return hasMatchingOccupant ? State.Succeeded : State.Failed;
        }
    }
}