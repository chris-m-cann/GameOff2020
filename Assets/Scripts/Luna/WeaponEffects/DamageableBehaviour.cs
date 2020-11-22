using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using UltEvents;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Unit.Unit))]
    public class DamageableBehaviour : EffectHandler<DamageEffect>
    {
        public int health = 2;
        [SerializeField] private UltEvent<int> onDamage;
        public override List<IUnitAction> Handle(DamageEffect effect, GameObject wielder)
        {
            return new List<IUnitAction>(1){new DamageAction(GetComponent<Unit.Unit>(), effect)};
        }

        public void Damage(DamageEffect effect)
        {
            health -= effect.Damage;
            onDamage.Invoke(health);
            if (health <= 0) Destroy(gameObject);
        }
    }
}