using UnityEngine;

namespace Util.Ai.Bt
{
    public class ConstantStateNode : BtNode
    {
        [SerializeField] private State returnState;
        protected override State OnExecute(AgentContext context)
        {
            return returnState;
        }
    }
}