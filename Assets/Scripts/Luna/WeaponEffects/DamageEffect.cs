using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [CreateAssetMenu(menuName = "Custom/WeaponEffect/Damage")]
    public class DamageEffect : WeaponEffect
    {
        public int Damage = 1;
        // todo(add damage type)

        public override List<IUnitAction> Apply(GameObject target, GameObject wielder) =>
            DefaultApply<DamageEffect>(target, wielder, this);
    }
}