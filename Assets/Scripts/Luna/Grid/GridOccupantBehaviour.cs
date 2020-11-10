using System;
using System.Linq;
using UnityEngine;
using Util;

namespace Luna.Grid
{
    public class GridOccupantBehaviour : MonoBehaviour, IProvider<GridVariable>
    {
        [SerializeField] private GridOccupant occupant = new GridOccupant();
        [SerializeField] private GridVariable grid;

        private Vector2Int _currentIdx = Vector2Int.left;

        public Vector2Int CurrentNodeIdx => _currentIdx;

        private void Awake()
        {
            occupant.OccupantGameObject = gameObject;
        }

        public void UpdateGrid(Vector3 worldPos)
        {
            _currentIdx = grid.Value.MoveOccupant(_currentIdx, worldPos, occupant);
        }

        public void AddToGrid() => UpdateGrid(transform.position);

        public GridVariable Get() => grid;
    }
}