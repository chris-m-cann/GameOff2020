using System;

namespace Util.Ai.Bt
{
    [Serializable]
    public abstract class BtNode
    {
        public enum State
        {
            Succeeded,
            Failed,
            Running
        }

        public State CurrentState;

        public abstract State Execute(AgentContext context);
    }
}