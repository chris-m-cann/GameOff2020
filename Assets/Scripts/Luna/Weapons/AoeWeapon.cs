using System.Linq;
using UnityEngine;

namespace Luna.Weapons
{
    [CreateAssetMenu(menuName = "Custom/Weapon/Aoe")]
    public class AoeWeapon : Weapon
    {
        [SerializeField] private int area;

        public override Grid.Grid.Node[] FindTargets(Grid.Grid.Node wielder, Grid.Grid grid)
        {

            var neighbours = grid.GetNeighboursInRange(wielder, area);

            // todo(chris) consider optimising as this will be used by every mellee enemy on the grid
            return neighbours.Where(node => node.Occupants.Any(it => it.Tags.Intersect(TargetTypes).Any())).ToArray();
        }
    }
}