using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [CreateAssetMenu(menuName = "Custom/WeaponEffect/Chasm")]
    public class ChasmEffect : WeaponEffect
    {
        public override List<IUnitAction> Apply(GameObject target, GameObject wielder) =>
            DefaultApply(target, wielder, this);
    }
}