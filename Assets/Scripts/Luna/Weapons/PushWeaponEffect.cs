using System;
using System.Collections.Generic;
using Luna.Unit;
using UnityEngine;

namespace Luna.Weapons
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

        public override List<IUnitAction> Apply(GameObject target, GameObject wielder)
        {
            var handlers = target.GetComponents<EffectHandler<PushWeaponEffect>>();

            if (handlers == null) return null;

            var actions = new List<IUnitAction>();
            foreach (var handler in handlers)
            {
                var r = handler.Handle(this, wielder);
                if (r != null)
                {
                    actions.AddRange(r);
                }
            }

            return actions;
        }
    }
}