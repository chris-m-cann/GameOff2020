using System.Collections.Generic;
using Luna.Actions;
using Luna.Unit;
using Luna.Weapons;
using UltEvents;
using UnityEngine;
using Util;
using Util.Inventory;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Unit.Unit), typeof(IProvider<Inventory>))]
    public class DamageableBehaviour : EffectHandler<DamageEffect>
    {
        public InventoryKey healthKey;
        [SerializeField] private UltEvent<int> onDamage;
        [SerializeField] private UltEvent onDestroy;
        public override List<IUnitAction> Handle(DamageEffect effect, GameObject wielder)
        {
            return new List<IUnitAction>(1){new DamageAction(gameObject, effect)};
        }

        public void Damage(Unit.Unit unit, DamageEffect effect)
        {
            var inventory = GetComponent<IProvider<Inventory>>().Get();
            if (inventory != null && healthKey != null)
            {
                if (inventory.RetrieveSlot(healthKey, out AggregateSlot health))
                {
                    health.Total -= Mathf.Min(effect.Damage, health.Total);
                    inventory.UpdateSlot(healthKey, health);
                    onDamage.Invoke(health.Total);

                    if (health.Total > 0)
                    {
                        return;
                    }
                }
            }

            // if i get here i either have no health or have spent it all
            onDestroy.Invoke();

            unit.QueueAction(new DestroyGameObjectAction(gameObject));
        }
    }
}