using System;
using System.Collections.Generic;
using System.Linq;
using Luna.Grid;
using UnityEngine;

namespace Luna.Weapons
{
    [CreateAssetMenu(menuName = "Custom/Weapon/Aoe")]
    public class AoeWeapon : Weapon
    {
        [SerializeField] private int area;

        // public override Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid)
        // {
        //
        //     var neighbours = grid.GetNeighboursInRange(wielder, area);
        //
        //     // todo(chris) consider optimising as this will be used by every mellee enemy on the grid
        //     return neighbours.Where(node => node.Occupants.Any(it => it.Tags.Intersect(TargetTypes).Any())).ToArray();
        // }

        public override GridOccupant[] FindTargets(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var node = new Grid.Grid.Node();
            if (grid.TryGetNodeAt(wielder.Position, ref node))
            {
                var neighbours = grid.GetNeighboursInRange(node, area);

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
            var targets = FindTargets(wielder, target.Position - wielder.Position, grid);

            return Array.IndexOf(targets, target) > -1;
        }

        public override List<GridOccupant> FindAllPossibleTargets(GridOccupant wielder, Grid.Grid grid)
        {
            return FindTargets(wielder, Vector2Int.down, grid).ToList();
        }

        public override List<Grid.Grid.Node> FindAllPossibleEffectedNodes(GridOccupant wielder, Grid.Grid grid)
        {
            return FindEffectedNodes(wielder, Vector2Int.down, grid);
        }
    }
}