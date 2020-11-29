using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.WeaponEffects
{
    public class DestroyableBehaviour : EffectHandler<DestroyWielderEffect>
    {
        public override List<IUnitAction> Handle(DestroyWielderEffect effect, GameObject wielder)
        {
            return DestroyUnitAction();
        }

        public List<IUnitAction> DestroyUnitAction()
        {

            return new List<IUnitAction>
            {
                new RunActionAction(gameObject, () =>
                {
                    Destroy(gameObject);
                }, priority: 999)
            };
        }
    }
}