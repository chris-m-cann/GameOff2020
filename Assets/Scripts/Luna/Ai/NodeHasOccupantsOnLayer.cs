using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class NodeHasOccupantsOnLayer : BtNode
    {
        [SerializeField] private LayerMask layers;
        [SerializeField] private BlackboardKey nodeKey;

        protected override State OnExecute(AgentContext context)
        {
            var node = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(nodeKey);
            if (node == null) return State.Failed;

            var hasOccupantInLayerMask =
                node.Value.Occupants.Any(it =>
                {
                    var layerBit = 1 << it.OccupantGameObject.layer;

                    return (layers.value & layerBit) == layerBit;
                });

            return hasOccupantInLayerMask ? State.Succeeded : State.Failed;
        }
    }
}