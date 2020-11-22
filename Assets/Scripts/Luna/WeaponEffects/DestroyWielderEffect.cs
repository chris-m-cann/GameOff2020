using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.WeaponEffects
{

    [CreateAssetMenu(menuName = "Custom/WeaponEffect/DestroyWielder")]
    public class DestroyWielderEffect : WeaponEffect
    {
        public override List<IUnitAction> Apply(GameObject target, GameObject wielder) =>
            DefaultApply(wielder, wielder, this);
    }
}