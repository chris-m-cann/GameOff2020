using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;

namespace Luna.Weapons
{
    [CreateAssetMenu(menuName = "Custom/Weapon/Ranged")]
    public class RangedWeapon : Weapon
    {
        public struct PathDetails
         {
             public List<Grid.Grid.Node> Travelled;
             public bool IsLastNodeTarget;
         }

        [SerializeField] private ProjectileBehaviour projectile;



        public PathDetails CalculatePath(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            PathDetails path = new PathDetails
            {
                Travelled = new List<Grid.Grid.Node>(),
                IsLastNodeTarget = false
            };

            var normalised = direction.CardinalNormalise();

            var magnitude = projectile.Range;
            var node = new Grid.Grid.Node();

            for (int i = 1; i <= magnitude; i++)
            {
                if (grid.TryGetNodeAt(wielder.Position + i * normalised, ref node))
                {
                    path.Travelled.Add(node);

                    // if we have hit something that has a type then weve eiher hit something we cant move though
                    // or weve hit our target
                    if (node.Occupants.Any(it => it.Type != null))
                    {
                        path.IsLastNodeTarget = node.Occupants.Any(it => it.Type != null && TargetTypes.Contains(it.Type));
                        break;
                    }
                    // todo(chris) add back in when we implement penetration
                    // else
                    // {
                    //     var targetOccupants = node.Occupants.Where(it => TargetTypes.Contains(it.Type));
                    //
                    //     var gridOccupants = targetOccupants as GridOccupant[] ?? targetOccupants.ToArray();
                    //
                    //     // occupant isnt a target so cant go through
                    //     if (gridOccupants.Length == 0)
                    //     {
                    //         break;
                    //     }
                    //
                    //     path.Hits.Add(node);
                    //
                    //     var penetrating = gridOccupants.Where(occupant => projectile.PenitrationTags.Contains(occupant.Type));
                    //
                    //     var penetratingArray = penetrating as GridOccupant[] ?? penetrating.ToArray();
                    //
                    //     // cant penetrate target to this will be as far as we go
                    //     if (penetratingArray.Length == 0)
                    //     {
                    //         break;
                    //     }
                    //
                    //     // if we cant penetrate all then we are done
                    //     if (remainingDepth <= penetratingArray.Length)
                    //     {
                    //         break;
                    //     }
                    //
                    //    // continue;
                    // }
                }
                else
                {
                    break;
                }
            }

            return path;
        }


        public override GridOccupant[] FindTargets(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var path = CalculatePath(wielder, direction, grid);
            if (!path.IsLastNodeTarget) return null;

            var targets = path.Travelled.Last().Occupants.Where(it => TargetTypes.Contains(it.Type));
            return targets as GridOccupant[] ?? targets.ToArray();
        }

        public override bool CanTarget(GridOccupant target, GridOccupant wielder, Grid.Grid grid)
        {
            var path = CalculatePath(wielder, target.Position - wielder.Position, grid);
            return path.IsLastNodeTarget;
        }

        public override List<IUnitAction> Use(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            var path = CalculatePath(wielder, direction, grid);

            if (path.Travelled.Count == 0) return null;
            var endPoint = path.Travelled.Last();

            return new List<IUnitAction>{new LaunchProjectileAction(wielder, endPoint, projectile)};
        }
    }
}