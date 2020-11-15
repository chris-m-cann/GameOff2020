namespace Luna.Unit
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