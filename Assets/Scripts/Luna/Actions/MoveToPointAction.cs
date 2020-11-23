using Luna.Grid;
using Luna.Unit;
using UnityEngine.UI;

namespace Luna.Actions
{
    public class MoveToPointAction : IUnitAction
    {
        private readonly Grid.Grid.Node _destination;
        private readonly bool _twoWayCollisions;
        private readonly Unit.Unit _unit;

        private bool _isFinished;


        public MoveToPointAction(Unit.Unit unit, Grid.Grid.Node destination, bool twoWayCollisions = false)
        {
            _destination = destination;
            _twoWayCollisions = twoWayCollisions;
            _unit = unit;
        }

        public void StartAction(Unit.Unit unit)
        {
            var move = _unit.GetComponent<MoveAlongPath>();
            if (move == null)
            {
                _isFinished = true;
                return;
            }


            move.Move(_unit, _destination, () =>
            {
                _unit.Occupant.UpdateGrid(_unit.transform.position);
                _isFinished = true;
            }, _twoWayCollisions);
        }

        public bool Tick(Unit.Unit unit)
        {
            return _isFinished;
        }

        public int Priority { get; }
    }
}