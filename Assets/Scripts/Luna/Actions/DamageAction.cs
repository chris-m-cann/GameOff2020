using Luna.Unit;
using Luna.WeaponEffects;

namespace Luna.Actions
{
    public class DamageAction : BaseAction
    {
        private DamageEffect _effect;

        private DamageableBehaviour _damageable;

        public DamageAction(Unit.Unit unit, DamageEffect effect): base(unit)
        {
            _effect = effect;
            _damageable = unit.GetComponent<DamageableBehaviour>();
        }

        public override void Execute()
        {
            IsStarted = true;

            _damageable.Damage(_effect);
            IsFinished = true;
        }

        public override TurnPhase Phase => TurnPhase.ResolvingAction;
    }
}