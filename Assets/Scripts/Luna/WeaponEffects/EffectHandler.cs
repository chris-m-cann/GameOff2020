using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Unit.Unit))]
    public abstract class EffectHandler<T> : MonoBehaviour where T : WeaponEffect
    {
        protected Unit.Unit _unit;

        protected virtual void Awake()
        {
            _unit = GetComponent<Unit.Unit>();
        }

        public abstract List<IUnitAction> Handle(T effect, GameObject wielder);
    }
}