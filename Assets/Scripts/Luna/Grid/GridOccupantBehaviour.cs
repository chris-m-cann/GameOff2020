using System;
using System.Linq;
using UltEvents;
using UnityEngine;
using Util;

namespace Luna.Grid
{
    public class GridOccupantBehaviour : MonoBehaviour, IProvider<GridVariable>
    {
        public UltEvent<GridOccupantBehaviour> OnRemove;

        public GridOccupant Occupant = new GridOccupant();
        [SerializeField] private GridVariable grid;

        public Vector2Int CurrentNodeIdx => Occupant.Position;

        public Grid.Node? CurrentNode
        {
            get
            {
                var n = new Grid.Node();
                if (grid.Value.TryGetNodeAt(CurrentNodeIdx.x, CurrentNodeIdx.y, ref n))
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

            if (grid != null && grid.Value != null)
            {
                AddToGrid();
            }
        }

        public void UpdateGrid(Vector3 worldPos)
        {
            grid.Value.MoveOccupant(worldPos, Occupant);
        }

        public void AddToGrid() => UpdateGrid(transform.position);

        public GridVariable Get() => grid;

        private void OnDestroy()
        {
            OnRemove?.Invoke(this);
            RemoveFromGrid();
        }

        private void RemoveFromGrid()
        {
            grid.Value.RemoveOccupant(Occupant);
        }
    }
}