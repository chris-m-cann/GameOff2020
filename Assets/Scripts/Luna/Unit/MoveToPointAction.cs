using Luna.Grid;

namespace Luna.Unit
{
    public class MoveToPointAction : IUnitAction
    {
        private readonly Grid.Grid.Node _destination;

        private readonly MoveAlongPath _move;
        private readonly GridOccupantBehaviour _occupant;

        public MoveToPointAction(Unit unit, Grid.Grid.Node destination)
        {
            _destination = destination;

            _move = unit.GetComponent<MoveAlongPath>();
            _occupant = unit.GetComponent<GridOccupantBehaviour>();
        }

        public void Execute()
        {
            IsStarted = true;

            if (_move == null)
            {
                IsFinished = true;
                return;
            }
            
            _occupant?.UpdateGrid(_destination.WorldPosition);
            _move.Move(_destination, () => IsFinished = true);
        }

        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public TurnPhase Phase { get => TurnPhase.PerformingAction; }
    }
}