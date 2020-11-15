using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [CreateAssetMenu(menuName = "Custom/WeaponEffect/Push")]
    public class PushWeaponEffect : WeaponEffect
    {
        // todo(chris) custom property draw as dont want to show custom direction in inspector if not being used
        public int PushMagnitude;
        public PushDirection Direction;
        public Vector2Int CustomDirection;

        public enum PushDirection
        {
            AwayFromWielder,
            TowardsWielder,
            CustomDirection
        }

        public override List<IUnitAction> Apply(GameObject target, GameObject wielder) =>
            DefaultApply<PushWeaponEffect>(target, wielder, this);
    }
}