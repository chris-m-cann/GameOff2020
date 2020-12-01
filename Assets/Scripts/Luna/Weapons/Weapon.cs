using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using Luna.Unit;
using Luna.WeaponEffects;
using UnityEngine;
using Util;
using Util.Inventory;

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

        public virtual List<Grid.Grid.Node> FindEffectedNodes(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var occupants = FindTargets(wielder, direction, grid);
            var nodes = new List<Grid.Grid.Node>();

            foreach (var occupant in occupants)
            {
                if (nodes.FindIndex(it => it.Position == occupant.Position) > -1)
                {
                    var n = new Grid.Grid.Node();
                    if (grid.TryGetNodeAt(occupant.Position, ref n))
                    {
                        nodes.Add(n);
                    }
                }
            }

            return nodes;
        }


        public virtual List<GridOccupant> FindAllPossibleTargets(GridOccupant wielder, Grid.Grid grid)
        {
            var occupants = new List<GridOccupant>();

            occupants.AddNullableRange(FindTargets(wielder, Vector2Int.up, grid));
            occupants.AddNullableRange(FindTargets(wielder, Vector2Int.down, grid));
            occupants.AddNullableRange(FindTargets(wielder, Vector2Int.left, grid));
            occupants.AddNullableRange(FindTargets(wielder, Vector2Int.right, grid));

            return occupants;
        }

        public virtual List<Grid.Grid.Node> FindAllPossibleEffectedNodes(GridOccupant wielder, Grid.Grid grid)
        {
            var occupants = new List<Grid.Grid.Node>();

            occupants.AddNullableRange(FindEffectedNodes(wielder, Vector2Int.up, grid));
            occupants.AddNullableRange(FindEffectedNodes(wielder, Vector2Int.down, grid));
            occupants.AddNullableRange(FindEffectedNodes(wielder, Vector2Int.left, grid));
            occupants.AddNullableRange(FindEffectedNodes(wielder, Vector2Int.right, grid));

            return occupants;
        }

        public virtual void OnEquip(Unit.Unit unit, InventoryKey weaponKey)
        {

        }

        public virtual void OnUnequip(Unit.Unit unit, InventoryKey weaponKey)
        {

        }
    }
}