using Luna.Grid;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    public class PushableBehaviour : EffectHandler<PushWeaponEffect>
    {
        private GridOccupantBehaviour _occupant;
        private MoveAlongPath _mover;

        private void Awake()
        {
            _mover = GetComponent<MoveAlongPath>();
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public override void Handle(PushWeaponEffect effect, GameObject wielder)
        {
            var newPos = transform.position + (Vector3)effect.CustomDirection;

            _mover.Move(newPos, () => _occupant.UpdateGrid(newPos));
        }
    }
}