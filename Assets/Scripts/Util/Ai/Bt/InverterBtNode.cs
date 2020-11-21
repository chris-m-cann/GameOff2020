namespace Util.Ai.Bt
{
    public class InverterBtNode : BtDecoratorNode
    {
        protected override State OnExecute(AgentContext context)
        {
            if (child == null) return State.Failed;

            switch (child.Execute(context))
            {
               case State.Succeeded: return State.Failed;
               case State.Failed: return State.Succeeded;
               default: return State.Failed;
            }
        }
    }
}