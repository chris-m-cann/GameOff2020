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
        public GridOccupantType[] TargetTypes;
        public WeaponEffect[] Effects;
        public bool EffectSelf;

        // public abstract Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid);
        //
        // public virtual List<IUnitAction> Apply(Grid.Grid.Node target, GameObject wielder)
        // {
        //     var actions = new List<IUnitAction>();
        //     foreach (var occupant in target.Occupants)
        //     {
        //         if (occupant.OccupantGameObject != wielder || EffectSelf)
        //         {
        //             foreach (var effect in Effects)
        //             {
        //                 var r = effect.Apply(occupant.OccupantGameObject, wielder);
        //                 actions.AddNullableRange(r);
        //             }
        //         }
        //     }
        //
        //     return actions;
        // }

        public abstract GridOccupant[] FindTargets(GridOccupant wielder, Vector2Int direction, Grid.Grid grid);
        public abstract bool CanTarget(GridOccupant target, GridOccupant wielder, Grid.Grid grid);

        public virtual List<IUnitAction> Use(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var actions = new List<IUnitAction>();

            var targets = FindTargets(wielder, direction, grid);

            foreach (var occupant in targets)
            {
                if (occupant != wielder || EffectSelf)
                {
                    foreach (var effect in Effects)
                    {
                        var r = effect.Apply(occupant.OccupantGameObject, wielder.OccupantGameObject);
                        actions.AddNullableRange(r);
                    }
                }
            }

            return actions;
        }


    }
}