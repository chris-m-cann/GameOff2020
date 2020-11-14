using System.Collections.Generic;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    public abstract class EffectHandler<T> : MonoBehaviour where T : WeaponEffect
    {
        public abstract List<IUnitAction> Handle(T effect, GameObject wielder);
    }
}