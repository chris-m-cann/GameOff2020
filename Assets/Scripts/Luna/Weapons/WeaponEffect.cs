using System.Collections.Generic;
using Luna.Unit;
using UnityEngine;

namespace Luna.Weapons
{
    public abstract class WeaponEffect : ScriptableObject
    {
        public abstract List<IUnitAction> Apply(GameObject target, GameObject wielder);
    }
}