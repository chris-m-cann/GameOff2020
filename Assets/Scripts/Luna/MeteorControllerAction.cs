using System;
using Luna.Actions;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    public class MeteorControllerAction : ActionBehaviour
    {
        public event Action<MeteorControllerAction> OnMeteorDestroyed;

        [SerializeField] private AoeWeapon weapon;
        [SerializeField] private Sprite[] stages;
        [SerializeField] private SpriteRenderer renderer;

        private int _turnsSinceSpawn = 0;
        public override void StartAction(Unit.Unit unit)
        {
            ++_turnsSinceSpawn;
            if (_turnsSinceSpawn > stages.Length)
            {
                DoEffect(unit);
                OnMeteorDestroyed?.Invoke(this);
                unit.QueueAction(new DestroyGameObjectAction(gameObject));
            }
            else
            {
                renderer.sprite = stages[_turnsSinceSpawn - 1];
            }
        }

        private void DoEffect(Unit.Unit unit)
        {
            unit.QueueRange(weapon.Use(unit.Occupant.Occupant, Vector2Int.zero, unit.Occupant.Grid));
        }

        public override bool Tick(Unit.Unit actor)
        {
            return true;
        }
    }
}