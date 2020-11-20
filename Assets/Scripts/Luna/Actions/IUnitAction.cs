using System.Collections.ObjectModel;

namespace Luna.Actions
{
    public interface IUnitAction
    {
        void Execute();

        void Reset();
        bool IsStarted { get; }
        bool IsFinished { get; }
        TurnPhase Phase { get; }
    }
}