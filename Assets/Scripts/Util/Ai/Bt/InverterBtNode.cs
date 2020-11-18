namespace Util.Ai.Bt
{
    public class InverterBtNode : BtNode
    {
        protected BtNode BtNode;

        public InverterBtNode(BtNode btNode)
        {
            BtNode = btNode;
        }

        public override State Execute(AgentContext context)
        {
            switch (BtNode.Execute(context))
            {
               case State.Succeeded: return State.Failed;
               case State.Failed: return State.Succeeded;
               default: return State.Failed;
            }
        }
    }
}