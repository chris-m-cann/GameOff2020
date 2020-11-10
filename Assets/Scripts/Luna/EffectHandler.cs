using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    public abstract class EffectHandler<T> : MonoBehaviour where T : WeaponEffect
    {
        public abstract void Handle(T effect, GameObject wielder);
    }
}