using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;
using Util.Inventory;

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
        [SerializeField] private AggregateItem initialAmmunition;
        [SerializeField] private bool unequipWhenOutOfAmmo = true;
        // todo(chris) scriptable objects should hold state but should be fine in this case as all will be using the same weapon key
        // still bad design though. do the fixing
        private InventoryKey _weaponKey;
        public int Range => projectile.Range;

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

            if (path.IsLastNodeTarget && path.Travelled.Count == 0)
            {
                path = new PathDetails
                {
                    Travelled = new List<Grid.Grid.Node>(),
                    IsLastNodeTarget = false
                };
            }
            else
            {
                path.Travelled.RemoveAt(0);
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

            var outOfAmmo = true;
            var spawnProjectile = true;

            var inventory = wielder.OccupantGameObject.GetComponent<IProvider<Inventory>>()?.Get();
            if (inventory != null)
            {
                if (initialAmmunition != null)
                {
                    AggregateSlot ammuntion;
                    if (inventory.RetrieveSlot(initialAmmunition.Key, out ammuntion))
                    {
                        if (ammuntion.Total < 1)
                        {
                            outOfAmmo = true;
                            spawnProjectile = false;
                        }
                        else if (ammuntion.Total == 1)
                        {
                            outOfAmmo = true;
                            spawnProjectile = true;
                        }
                        else
                        {
                            ammuntion.Total--;
                            inventory.UpdateSlot(initialAmmunition.Key, ammuntion);
                            spawnProjectile = true;
                            outOfAmmo = false;
                        }
                    }
                }

                if (outOfAmmo && unequipWhenOutOfAmmo)
                {
                    inventory.RemoveWeaponSlot(_weaponKey);
                }
            }

            if (spawnProjectile)
            {
                return new List<IUnitAction>{new LaunchProjectileAction(wielder, endPoint, projectile)};
            }
            else
            {
                return null;
            }
        }

        public override List<Grid.Grid.Node> FindEffectedNodes(GridOccupant wielder, Vector2Int direction, Grid.Grid grid)
        {
            return CalculatePath(wielder, direction, grid).Travelled;
        }

        public override void OnEquip(Unit.Unit unit, InventoryKey weaponKey)
        {
            base.OnEquip(unit, weaponKey);
            _weaponKey = weaponKey;

            if (initialAmmunition != null)
            {
                var inventory = unit.GetComponent<IProvider<Inventory>>()?.Get();

                if (inventory != null)
                {
                    initialAmmunition.AddToInventory(inventory);
                }
            }
        }


        public static RangedWeapon CreateInstance(ProjectileBehaviour projectile)
        {
            var instance = CreateInstance<RangedWeapon>();
            instance.projectile = projectile;

            return instance;
        }
    }
}