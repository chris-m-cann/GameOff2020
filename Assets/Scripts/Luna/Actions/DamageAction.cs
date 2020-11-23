using Luna.Unit;
using Luna.WeaponEffects;
using UnityEngine;

namespace Luna.Actions
{
    public class DamageAction : IUnitAction
    {
        private readonly DamageEffect _effect;
        private readonly GameObject _effected;

        public DamageAction(GameObject thingToDamage, DamageEffect effect)
        {
            _effect = effect;
            _effected = thingToDamage;
        }

        public void StartAction(Unit.Unit unit)
        {
            _effected.GetComponent<DamageableBehaviour>().Damage(_effect);
        }

        public bool Tick(Unit.Unit actor)
        {
            return true;
        }

        public int Priority { get; }
    }
}