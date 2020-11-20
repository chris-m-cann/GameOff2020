using System.Collections.Generic;

namespace Util.Ai.Bt
{
    public class SequenceBtNode : BtCompositeNode
    {
        public SequenceBtNode()
        {
        }

        public SequenceBtNode(List<BtNode> nodes)
        {
            children = nodes;
        }
        public override State Execute(AgentContext context)
        {
            var areNodesRunning = false;
            foreach (var node in children)
            {
                switch (node.Execute(context))
                {
                    case State.Failed: return State.Failed;
                    case State.Running:
                        areNodesRunning = true;
                        break;
                    default:
                        break;
                }
            }

            return areNodesRunning ? State.Running : State.Succeeded;
        }
    }
}