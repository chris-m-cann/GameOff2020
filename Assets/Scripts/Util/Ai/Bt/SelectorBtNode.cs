using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Util.Ai.Bt
{
    public class SelectorBtNode : BtCompositeNode
    {
        protected override State OnExecute(AgentContext context)
        {
            foreach (var child in children)
            {
                switch (child.Execute(context))
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