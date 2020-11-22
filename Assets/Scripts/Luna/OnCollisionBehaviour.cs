using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    public class OnCollisionBehaviour : MonoBehaviour
    {
        public Weapon OnCollisionEffects;


        public List<IUnitAction> CollideWith(GridOccupant other, Grid.Grid.Node otherNode)
        {
            Debug.Log($"Collide with for {name} on {other.OccupantGameObject.name}");
            // if I hurt things on collision then hurt them
            if (OnCollisionEffects?.TargetTypes?.Intersect(other.Tags)?.Any() ==
                true)
            {
                Debug.Log($"Collide with for {name} applying effects to {other.OccupantGameObject.name}");
                return OnCollisionEffects.Apply(otherNode, gameObject);
            }
            else
            {
                return null;
            }
        }
    }
}