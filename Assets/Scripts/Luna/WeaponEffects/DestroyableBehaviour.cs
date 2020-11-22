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
                new RunActionAction(_unit, TurnPhase.EndOfTurn, () =>
                {
                    Destroy(gameObject);
                })
            };
        }
    }
}