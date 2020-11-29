using System;
using Luna.Grid;
using UnityEngine;

namespace Luna.Weapons
{
    [RequireComponent(typeof(MoveAlongPath), typeof(Unit.Unit))]
    public class ProjectileBehaviour : MonoBehaviour
    {
        public int Range = 3;

        // todo(chris) add back in when doing penitration
        // // how many targets it can hit
        // public int PenitrationDepth;
        // // what types of target can it penitrate
        // public GridOccupantType[] PenitrationTags;

        private MoveAlongPath _move;
        private Unit.Unit _unit;

        private void Awake()
        {
            _move = GetComponent<MoveAlongPath>();
            _unit = GetComponent<Unit.Unit>();
        }

        public void Fire(Grid.Grid.Node destination, Action onComplete)
        {
            _move.Move(_unit, destination, () =>
            {
                // spawn item??
                // destroy
                _unit.Occupant.UpdateGrid(_unit.transform.position);
                onComplete();
            });
        }
    }
}