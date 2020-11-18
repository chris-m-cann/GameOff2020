using System.Collections.Generic;

namespace Util.Ai.Bt
{
    public class SelectorBtNode : BtNode
    {
        protected List<BtNode> _nodes;

        public SelectorBtNode(List<BtNode> nodes)
        {
            _nodes = nodes;
        }
        public override State Execute(AgentContext context)
        {
            foreach (var node in _nodes)
            {
                switch (node.Execute(context))
                {
                    case State.Succeeded: return State.Succeeded;
                    case State.Running: return State.Running;
                    default:
                        break;
                }
            }

            return State.Failed;
        }
    }
}