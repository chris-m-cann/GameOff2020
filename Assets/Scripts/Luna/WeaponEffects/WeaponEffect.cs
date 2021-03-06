using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using UnityEngine;
using Util;

namespace Luna.WeaponEffects
{
    public abstract class WeaponEffect : ScriptableObject
    {
        public abstract List<IUnitAction> Apply(GameObject target, GameObject wielder);
        protected List<IUnitAction> DefaultApply<T>(GameObject target, GameObject wielder, T effect) where T : WeaponEffect
        {
            var handlers = target.GetComponents<EffectHandler<T>>();

            if (handlers == null) return null;

            var actions = new List<IUnitAction>();
            foreach (var handler in handlers)
            {
                var r = handler.Handle(effect, wielder);
                actions.AddNullableRange(r);
            }

            return actions;
        }
    }
}