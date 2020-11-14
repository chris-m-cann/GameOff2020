using System.Collections.Generic;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    [RequireComponent(typeof(Unit.Unit), typeof(GridOccupantBehaviour))]
    public class PushableBehaviour : EffectHandler<PushWeaponEffect>
    {
        private GridOccupantBehaviour _occupant;
        private MoveAlongPath _mover;
        private Unit.Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit.Unit>();
            _mover = GetComponent<MoveAlongPath>();
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public override List<IUnitAction> Handle(PushWeaponEffect effect, GameObject wielder)
        {
            var newPos = _occupant.CurrentNodeIdx + effect.CustomDirection;

            var node = new Grid.Grid.Node();
            if (_occupant.Get().Value.TryGetNodeAt(newPos.x, newPos.y, ref node))
            {
                return new List<IUnitAction>(1){new MoveToPointAction(_unit, node)};
            }
            else
            {
                return null;
            }
        }
    }
}