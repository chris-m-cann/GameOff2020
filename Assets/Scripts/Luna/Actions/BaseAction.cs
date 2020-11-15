using System.Collections;
using Luna.Unit;

namespace Luna.Actions
{
    public abstract class BaseAction : IUnitAction
    {
        protected Unit.Unit _unit;

        protected BaseAction(Unit.Unit unit)
        {
            _unit = unit;
        }
        public virtual void Execute()
        {
            IsStarted = true;

            _unit.StartCoroutine(CoExecute());
        }

        private IEnumerator CoExecute()
        {
            yield return _unit.StartCoroutine(ExecuteImpl());
            IsFinished = true;
        }

        protected virtual IEnumerator ExecuteImpl()
        {
            yield return null;
        }

        public abstract TurnPhase Phase { get; }



        public void Reset()
        {
            IsFinished = false;
            IsStarted = false;
        }

        public bool IsStarted { get; protected set; }
        public bool IsFinished { get; protected set; }
    }
}