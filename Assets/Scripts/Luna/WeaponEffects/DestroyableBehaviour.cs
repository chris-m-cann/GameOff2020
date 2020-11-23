using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.WeaponEffects
{
    public class DestroyableBehaviour : EffectHandler<DestroyWielderEffect>
    {
        public override List<IUnitAction> Handle(DestroyWielderEffect effect, GameObject wielder)
        {

            return new List<IUnitAction>
            {
                new RunActionAction(() =>
                {

                    // todo(chris) gross and wrong. no delays!! current thid ids to stop it destroying itself before action can complete
                    Destroy(gameObject, 1);
                }, priority: 999)
            };
        }
    }
}