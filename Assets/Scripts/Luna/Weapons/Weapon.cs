using Luna.Grid;
using UnityEngine;

namespace Luna.Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        public GridOccupantTag[] TargetTypes;
        public WeaponEffect[] Effects;

        public abstract Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid);

        public virtual void Apply(Grid.Grid.Node target, GameObject wielder)
        {
            foreach (var occupant in target.Occupants)
            {
                foreach (var effect in Effects)
                {
                    effect.Apply(occupant.OccupantGameObject, wielder);
                }
            }
        }
    }
}