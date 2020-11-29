using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using Luna.Unit;
using Luna.Weapons;
using UnityEngine;

namespace Luna
{
    [RequireComponent(typeof(GridOccupantBehaviour))]
    public class OnCollisionBehaviour : MonoBehaviour
    {
        public Weapon OnCollisionEffects;

        private GridOccupantBehaviour _occupant;

        private void Awake()
        {
            _occupant = GetComponent<GridOccupantBehaviour>();
        }

        public List<IUnitAction> CollideWith(GridOccupant other, Grid.Grid.Node otherNode)
        {
            // if I hurt things on collision then hurt them
            if (OnCollisionEffects?.CanTarget(other, _occupant.Occupant, _occupant.Grid) == true)
            {
                var diff = new Vector2Int(otherNode.X, otherNode.Y) - _occupant.CurrentNodeIdx;
                return OnCollisionEffects.Use(_occupant.Occupant, diff, _occupant.Grid);
            }
            else
            {
                return null;
            }
        }
    }
}