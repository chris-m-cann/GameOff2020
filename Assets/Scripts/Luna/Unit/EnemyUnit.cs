// using System.Collections.Generic;
// using Luna.Actions;
// using Luna.Grid;
// using UnityEngine;
//
// namespace Luna.Unit
// {
//     public class EnemyUnit : BaseUnit
//     {
//         private bool _upNext;
//
//         private GridOccupantBehaviour _occupant;
//
//         private void Awake()
//         {
//             _occupant = GetComponent<GridOccupantBehaviour>();
//         }
//
//         public override List<IUnitAction> StartTurn()
//         {
//             var nexPos = _upNext ? Vector2Int.up : Vector2Int.down;
//             _upNext = !_upNext;
//
//             nexPos = _occupant.CurrentNodeIdx + nexPos;
//
//             Grid.Grid.Node nextNode = new Grid.Grid.Node();
//
//             if (_occupant.Get().Value.TryGetNodeAt(nexPos.x, nexPos.y, ref nextNode))
//             {
//                 return new List<IUnitAction>(1){new MoveToPointAction(this, nextNode)};
//             }
//
//             return null;
//         }
//     }
// }