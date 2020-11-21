using System.Collections.Generic;

namespace Util.Ai.Bt
{
    public class SequenceBtNode : BtCompositeNode
    {
        protected override State OnExecute(AgentContext context)
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