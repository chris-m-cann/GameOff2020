namespace Luna.Unit
{
    public interface IUnitAction
    {
        void Execute();
        bool IsStarted { get; }
        bool IsFinished { get; }
        TurnPhase Phase { get; }
    }
}