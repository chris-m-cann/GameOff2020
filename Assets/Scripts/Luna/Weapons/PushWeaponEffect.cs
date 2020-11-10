using System;
using UnityEngine;

namespace Luna.Weapons
{
    [CreateAssetMenu(menuName = "Custom/WeaponEffect/Push")]
    public class PushWeaponEffect : WeaponEffect
    {
        // todo(chris) custom property draw as dont want to show custom direction in inspector if not being used
        public int PushMagnitude;
        public PushDirection Direction;
        public Vector2 CustomDirection;

        public enum PushDirection
        {
            AwayFromWielder,
            TowardsWielder,
            CustomDirection
        }

        public override void Apply(GameObject target, GameObject wielder)
        {
            var handlers = target.GetComponents<EffectHandler<PushWeaponEffect>>();

            if (handlers == null) return;
            else
            {
                foreach (var handler in handlers)
                {
                    handler.Handle(this, wielder);
                }
            }
        }
    }
}