using System.Collections.Generic;
using Luna.Actions;
using Luna.Grid;
using Luna.Unit;
using Luna.WeaponEffects;
using UnityEngine;
using Util;

namespace Luna.Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        public GridOccupantTag[] TargetTypes;
        public WeaponEffect[] Effects;

        public abstract Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid);

        public virtual List<IUnitAction> Apply(Grid.Grid.Node target, GameObject wielder)
        {
            var actions = new List<IUnitAction>();
            foreach (var occupant in target.Occupants)
            {
                if (occupant.OccupantGameObject != wielder)
                {
                    foreach (var effect in Effects)
                    {
                        var r = effect.Apply(occupant.OccupantGameObject, wielder);
                        actions.AddNullableRange(r);
                    }
                }
            }

            return actions;
        }
    }
}