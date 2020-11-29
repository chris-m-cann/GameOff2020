using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Unit.Unit))]
    [RequireComponent(typeof(DestroyableBehaviour))]
    public class ChasmEffectController : EffectHandler<ChasmEffect>
    {
        [SerializeField] private bool canFly;

        private DestroyableBehaviour _destroyable;

        protected override void Awake()
        {
            _destroyable = GetComponent<DestroyableBehaviour>();
        }

        public override List<IUnitAction> Handle(ChasmEffect effect, GameObject wielder)
        {
            // todo(chris) eventually remove shadow beneath flying unit when they have one
            if (canFly) return null;

            //todo(chris) death animation
            return _destroyable.DestroyUnitAction();
        }
    }
}