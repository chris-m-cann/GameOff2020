using UnityEngine;

namespace Util.Ai.Bt
{
    public class LoopNode : BtDecoratorNode
    {
        [SerializeField] private int iterations = 2;
        [SerializeField] private State continueWhileChildStateIs = State.Failed;
        [SerializeField] private State returnAfterLastIteration = State.Succeeded;
        protected override State OnExecute(AgentContext context)
        {
            int iteration = 0;
            State childState = State.Succeeded;
            while (iteration != iterations)
            {
                ++iteration;

                childState = child.Execute(context);

                if (childState != continueWhileChildStateIs)
                {
                    return childState;
                }
            }

            return returnAfterLastIteration;
        }
    }
}