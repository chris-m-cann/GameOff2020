using System.Collections.Generic;

namespace Util.Ai.Bt
{
    public class SequenceBtNode : BtNode
    {
        protected List<BtNode> _nodes;

        public SequenceBtNode(List<BtNode> nodes)
        {
            _nodes = nodes;
        }
        public override State Execute(AgentContext context)
        {
            var areNodesRunning = false;
            foreach (var node in _nodes)
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