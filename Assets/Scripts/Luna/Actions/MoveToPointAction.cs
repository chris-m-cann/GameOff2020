using Luna.Grid;
using Luna.Unit;

namespace Luna.Actions
{
    public class MoveToPointAction : IUnitAction
    {
        private readonly Grid.Grid.Node _destination;
        private readonly bool _twoWayCollisions;
        private readonly TurnPhase _phase;

        private readonly MoveAlongPath _move;
        private readonly GridOccupantBehaviour _occupant;


        public MoveToPointAction(Unit.Unit unit, Grid.Grid.Node destination, bool twoWayCollisions = false, TurnPhase phase = TurnPhase.Moving)
        {
            _destination = destination;
            _twoWayCollisions = twoWayCollisions;
            _phase = phase;

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
            

            _move.Move(_destination, () =>
            {
                _occupant?.UpdateGrid(_occupant.transform.position);
                IsFinished = true;
            }, _twoWayCollisions);
        }

        public void Reset()
        {
            IsStarted = false;
            IsFinished = false;
        }

        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public TurnPhase Phase => _phase;
    }
}