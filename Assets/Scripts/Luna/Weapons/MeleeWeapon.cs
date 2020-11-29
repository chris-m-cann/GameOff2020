using System;
using System.Collections.Generic;
using System.Linq;
using Luna.Grid;
using UnityEngine;
using Util;

namespace Luna.Weapons
{

    [CreateAssetMenu(menuName = "Custom/Weapon/Mellee")]
    public class MeleeWeapon : Weapon
    {
        // public override Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid)
        // {
        //     var neighbours = grid.GetNeighbours(wielder);
        //
        //     // todo(chris) consider optimising as this will be used by every mellee enemy on the grid
        //     return neighbours.Where(node => node.Occupants.Any(it => it.Tags.Intersect(TargetTypes).Any())).ToArray();
        // }

        // todo(chris) shouldnt this only find targets in direction??
        public override GridOccupant[] FindTargets(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var node = new Grid.Grid.Node();
            if (grid.TryGetNodeAt(wielder.Position, ref node))
            {
                var neighbours = grid.GetNeighbours(node);

                var targets = new List<GridOccupant>();

                foreach (var neighbour in neighbours)
                {
                    var validOccupants = neighbour.Occupants.Where(it => TargetTypes.Contains(it.Type));
                    targets.AddRange(validOccupants);
                }

                return targets.ToArray();
            }

            return null;
        }

        public override bool CanTarget(GridOccupant target, GridOccupant wielder, Grid.Grid grid)
        {
            var direction = target.Position - wielder.Position;

            if (!direction.IsCardinal() || direction.CardinalMagnitude() > 1) return false;

            var node = new Grid.Grid.Node();
            if (grid.TryGetNodeAt(wielder.Position, ref node))
            {
                return node.Occupants.Any(it => TargetTypes.Contains(it.Type));
            }

            return false;
        }
    }
}