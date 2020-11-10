using UnityEngine;

namespace Luna.Weapons
{
    public abstract class WeaponEffect : ScriptableObject
    {
        public abstract void Apply(GameObject target, GameObject wielder);
    }
}