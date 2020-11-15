using System.Collections.Generic;
using Luna.Unit;
using UnityEngine;

namespace Luna.WeaponEffects
{
    public abstract class EffectHandler<T> : MonoBehaviour where T : WeaponEffect
    {
        public abstract List<IUnitAction> Handle(T effect, GameObject wielder);
    }
}