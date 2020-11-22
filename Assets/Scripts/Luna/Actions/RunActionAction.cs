using System;
using System.Collections;

namespace Luna.Actions
{
    public class RunActionAction : BaseAction
    {
        private TurnPhase _phase;
        private Action _action;

        public RunActionAction(Unit.Unit unit, TurnPhase phase, Action action) : base(unit)
        {
            _phase = phase;
            _action = action;
        }

        protected override IEnumerator ExecuteImpl()
        {
            _action.Invoke();
            yield return null;
        }

        public override TurnPhase Phase => _phase;
    }
}