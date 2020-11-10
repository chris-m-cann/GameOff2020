using System;
using System.Linq;
using Luna.Grid;
using UnityEngine;

namespace Luna.Weapons
{

    [CreateAssetMenu(menuName = "Custom/Weapon/Mellee")]
    public class MeleeWeapon : Weapon
    {
        public override Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid)
        {
            var neighbours = grid.GetNeighbours(wielder);

            // todo(chris) consider optimising as this will be used by every mellee enemy on the grid
            return neighbours.Where(node => node.Occupants.Any(it => it.Tags.Intersect(TargetTypes).Any())).ToArray();
        }

    }
}