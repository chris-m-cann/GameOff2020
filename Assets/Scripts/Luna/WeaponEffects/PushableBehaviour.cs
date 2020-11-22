using System;
using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using TMPro;
using UnityEngine;
using Util;

namespace Luna.WeaponEffects
{
    [RequireComponent(typeof(Unit.Unit), typeof(GridOccupantBehaviour))]
    public class PushableBehaviour : EffectHandler<PushWeaponEffect>
    {
        private GridOccupantBehaviour _occupant;
        private Unit.Unit _unit;
        private OnCollisionBehaviour _collisionBehaviour;

        private void Awake()
        {
            _collisionBehaviour = GetComponent<OnCollisionBehaviour>();
            _unit = GetComponent<Unit.Unit>();
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public override List<IUnitAction> Handle(PushWeaponEffect effect, GameObject wielder)
        {
            var actions = new List<IUnitAction>();

            Vector2Int direction = GetPush(effect, wielder);

            if (direction == Vector2Int.zero)
            {
                Debug.Log($"{wielder.name} Tried To Push {name} (0, 0)!!");

                return actions;
            }

            var pos = _occupant.CurrentNodeIdx + direction;

            var node = new Grid.Grid.Node();
            if (_occupant.Get().Value.TryGetNodeAt(pos.x, pos.y, ref node))
            {
                // todo(chris) this doesnt really work with anything that isnt just in 1 direction
                actions.Add(new MoveToPointAction(_unit, node, twoWayCollisions: true, TurnPhase.ResolvingAction));
            }

            return actions;

            // Vector2Int magnitudes = new Vector2Int(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
            // Vector2Int signs = new Vector2Int(Math.Sign(direction.x), Math.Sign(direction.y));
            //
            // var pos = _occupant.CurrentNodeIdx;
            //
            // var step = Vector2Int.one;
            //
            // // step along push path one by one (including diagonals as 1)
            // // until we either collide with something or reach our final destination
            // while (magnitudes != Vector2Int.zero)
            // {
            //     if (magnitudes.x < 1)
            //     {
            //         step.x = 0;
            //     }
            //
            //     if (magnitudes.y < 1)
            //     {
            //         step.y = 0;
            //     }
            //
            //     magnitudes.x = Mathf.Max(0, magnitudes.x - 1);
            //     magnitudes.y = Mathf.Max(0, magnitudes.y - 1);
            //
            //     var next = pos + signs * step;
            //
            //
            //     var n = new Grid.Grid.Node();
            //     if (_occupant.Get().Value.TryGetNodeAt(next.x, next.y, ref n))
            //     {
            //         var collider = n.Occupants.FirstOrDefault(ItObstructsMe);
            //         if (collider != null)
            //         {
            //             ResolveCollision(actions, collider, n);
            //
            //             break;
            //         }
            //     }
            //
            //     pos = next;
            // }
            //
            //
            // var node = new Grid.Grid.Node();
            // if (_occupant.Get().Value.TryGetNodeAt(pos.x, pos.y, ref node))
            // {
            //     // todo(chris) this doesnt really work with anything that isnt just in 1 direction
            //     actions.Add(new MoveToPointAction(_unit, node));
            // }
            //
            // return actions;
        }

        private Vector2Int GetPush(PushWeaponEffect effect, GameObject wielder)
        {
            Vector2Int dir;
            switch (effect.Direction)
            {
                case PushWeaponEffect.PushDirection.AwayFromWielder:
                {
                    dir = GetPushAwayFromWielder(effect, wielder);
                    break;
                }
                case PushWeaponEffect.PushDirection.TowardsWielder:
                {
                    dir = -1 * GetPushAwayFromWielder(effect, wielder);
                    break;
                }
                case PushWeaponEffect.PushDirection.CustomDirection:
                    dir = effect.CustomDirection;
                    break;
                default:
                    dir = effect.CustomDirection;
                    break;
            }

            var v3 = new Vector3(dir.x, dir.y);
            return (wielder.transform.rotation * v3).ToVector2Int();
        }

        private Vector2Int GetPushAwayFromWielder(PushWeaponEffect effect, GameObject wielder)
        {
            var ocupant = wielder.GetComponent<GridOccupantBehaviour>().CurrentNode?.WorldPosition ??
                          wielder.transform.position;
            var diff = transform.position - (Vector3)ocupant;
            var diff2Int = diff.ToVector2Int();
            diff2Int.x = MathUtils.ClampInt(diff2Int.x, -1, 1);
            diff2Int.y = MathUtils.ClampInt(diff2Int.y, -1, 1);
            return effect.PushMagnitude * diff2Int;
        }

        private bool ItObstructsMe(GridOccupant occupant)
        {
            return occupant.Cost < 0;
        }

        private void ResolveCollision(List<IUnitAction> actions, GridOccupant collider, Grid.Grid.Node n)
        {
            // if i have on collision effects then run them
            actions.AddNullableRange(_collisionBehaviour?.CollideWith(collider, n));


            // if the thing i bumped has collision effects then run them
            var collision = collider.OccupantGameObject.GetComponent<OnCollisionBehaviour>();
            if (collision != null)
            {
                var myNode = _occupant.CurrentNode;
                if (myNode != null)
                {
                    actions.AddNullableRange(collision.CollideWith(_occupant.Occupant, myNode.Value));
                }
            }
        }
    }
}