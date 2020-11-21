using System;
using System.Linq;
using UnityEngine;
using Util;

namespace Luna.Grid
{
    public class GridOccupantBehaviour : MonoBehaviour, IProvider<GridVariable>
    {
        public GridOccupant Occupant = new GridOccupant();
        [SerializeField] private GridVariable grid;

        private Vector2Int _currentIdx = Vector2Int.left;

        public Vector2Int CurrentNodeIdx => _currentIdx;

        public Grid.Node? CurrentNode
        {
            get
            {
                var n = new Grid.Node();
                if (grid.Value.TryGetNodeAt(_currentIdx.x, _currentIdx.y, ref n))
                {
                    return n;
                }
                else
                {
                    return null;
                }
            }
        }

        public Grid Grid => grid.Value;

        private void Awake()
        {
            Occupant.OccupantGameObject = gameObject;
        }

        public void UpdateGrid(Vector3 worldPos)
        {
            _currentIdx = grid.Value.MoveOccupant(_currentIdx, worldPos, Occupant);
        }

        public void AddToGrid() => UpdateGrid(transform.position);

        public GridVariable Get() => grid;

        private void OnDestroy()
        {
            RemoveFromGrid();
        }

        private void RemoveFromGrid()
        {
            grid.Value.RemoveOccupantAtIdx(_currentIdx, Occupant);
        }
    }
}