using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Luna.Grid
{
    public abstract class Grid
    {
        public struct Node
        {
            public int Cost;
            public Vector2Int Position;
            public Vector2 WorldPosition;
            public ReadOnlyCollection<GridOccupant> Occupants => _occupants.AsReadOnly();


            public int X
            {
                get => Position.x;
                set => Position.x = value;
            }

            public int Y
            {
                get => Position.y;
                set => Position.y = value;
            }

            private readonly int _baseCost;
            private readonly List<GridOccupant> _occupants;

            public Node(int x, int y, int baseCost, Vector2 worldPosition, List<GridOccupant> occupants = null)
            {
                _baseCost = baseCost;
                Position = new Vector2Int(x, y);
                WorldPosition = worldPosition;
                _occupants = occupants ?? new List<GridOccupant>();
                Cost = _baseCost;
                UpdateCost();
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is Node node)
                {
                    return Cost == node.Cost && X == node.X && Y == node.Y && WorldPosition == node.WorldPosition;
                }
                else
                {
                    return false;
                }
            }

            private void UpdateCost()
            {
                if (Occupants == null) return;

                // todo (chris) need to change how we calculate cost. by doing min we may catch unwalkable sentianls like -1 but if we have a cost 1 item on a cost 2 tile we would want the max
                Cost = _baseCost;
                foreach (var occupant in _occupants)
                {
                    Cost = Mathf.Min(Cost, occupant.Cost);
                }

                //Debug.Log($"{WorldPosition} cost = {Cost}, occupants = {Occupants.Length}");
            }

            public void AddOccupant(GridOccupant occupant)
            {
                _occupants.Add(occupant);
                UpdateCost();
            }

            public void RemoveOccupant(GridOccupant occupant)
            {
                _occupants.Remove(occupant);
                UpdateCost();
            }
        }

        public abstract Vector2 Position00 { get; }

        public abstract bool TryGetNodeAt(int x, int y, ref Node node);
        public virtual bool TryGetNodeAt(Vector2Int pos, ref Node node) => TryGetNodeAt(pos.x, pos.y, ref node);
        public abstract bool TryGetNodeAtWorldPosition(Vector2 pos, ref Node node);
        public abstract Node[] GetNeighbours(Node node);
        public abstract bool HasTileAtPos(Vector2 target);
        public abstract Vector2 GetWorldPos(Node node);
        public abstract void UpdateNode(Node node);

        public abstract Vector2Int AddOccupant(Vector2 worldPosition, GridOccupant occupant);
        public abstract void RemoveOccupant(GridOccupant occupant);
        public abstract Vector2Int MoveOccupant(Vector2 newWorldPos, GridOccupant occupant);
        public abstract Node[] GetNeighboursInRange(Node node, int area);
    }
}
