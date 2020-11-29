using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Luna.Grid
{
    public class SquareGrid : Grid
    {
        private Node[,] _nodes;
        private int _width;
        private int _height;
        private Vector2 _position;

        public int Width => _width;
        public int Height => _height;

        public SquareGrid(Node[,] nodes, Vector2 positionOf00)
        {
            _nodes = nodes;
            _width = nodes.GetLength(0);
            _height = nodes.GetLength(1);
            _position = positionOf00;
        }

        public override bool TryGetNodeAt(int x, int y, ref Node node)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height) return false;
            node = _nodes[x, y];
            return true;
        }

        public override bool TryGetNodeAtWorldPosition(Vector2 pos, ref Node node)
        {
            var p = pos - _position;
            var idx = Mathf.FloorToInt(p.x);
            var idy = Mathf.FloorToInt(p.y);

            if (idx >= _width || idx < 0 && idy >= _height && idy < 0)
            {
                return false;
            }

            return TryGetNodeAt(idx, idy, ref node);
        }

        public override Node[] GetNeighbours(Node node)
        {
            Node[] neighbours = new Node[4];
            int count = 0;

            if (node.X > 0)
            {
                neighbours[count] = _nodes[node.X - 1, node.Y];
                count++;
            }

            if (node.Y > 0)
            {
                neighbours[count] = _nodes[node.X, node.Y - 1];
                count++;
            }

            if (node.X < (_width - 1))
            {
                neighbours[count] = _nodes[node.X + 1, node.Y];
                count++;
            }


            if (node.Y < (_height - 1))
            {
                neighbours[count] = _nodes[node.X, node.Y + 1];
                count++;
            }


            Array.Resize(ref neighbours, count);
            return neighbours;
        }

        public override Node[] GetNeighboursInRange(Node node, int area)
        {
            if (area < 1) return new Node[0];

            if (area == 1)
            {
                return new Node[1]{node};
            }

            var side = (2 * area) - 1;

            var neighboursCount = (side - 2) * (side - 2) + 4;
            var neighbours = new List<Node>(neighboursCount);

            neighbours.Add(node);

            var tmp = new Node();
            if (TryGetNodeAt(node.X - (area - 1), node.Y, ref tmp))
            {
                neighbours.Add(tmp);
            }

            if (TryGetNodeAt(node.X + (area - 1), node.Y, ref tmp))
            {
                neighbours.Add(tmp);
            }

            if (TryGetNodeAt(node.X, node.Y - (area - 1), ref tmp))
            {
                neighbours.Add(tmp);
            }

            if (TryGetNodeAt(node.X, node.Y + (area - 1), ref tmp))
            {
                neighbours.Add(tmp);
            }

            var startX = node.X - (area - 2);
            var endX = node.X - (area - 2);
            var startY = node.Y - (area - 2);
            var endY = node.Y - (area - 2);


            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if (TryGetNodeAt(x, y, ref node))
                    {
                        neighbours.Add(tmp);
                    }
                }
            }

            return neighbours.ToArray();
        }
        public override bool HasTileAtPos(Vector2 target)
        {
            var p = (target - _position);
            var idx = Mathf.FloorToInt(p.x);
            var idy = Mathf.FloorToInt(p.y);

            return (idx < _width && idx > -1) && (idy < _height && idy > -1);
        }

        public override Vector2 GetWorldPos(Node node)
        {
            return new Vector2(node.X + _position.x + .5f, node.Y + _position.y + .5f);
        }

        public override void UpdateNode(Node node)
        {
            if (node.X >= _width || node.X < 0 && node.Y >= _height && node.Y < 0)
            {
                return;
            }

            _nodes[node.X, node.Y] = node;
        }

        public override Vector2Int AddOccupant(Vector2 worldPosition, GridOccupant occupant)
        {
            var idx = GetNodeIndexAt(worldPosition);
            if (IdxIsOnGrid(idx))
            {
                occupant.Position = idx;
                _nodes[idx.x, idx.y].AddOccupant(occupant);
            }
            return idx;
        }

        public override void RemoveOccupant(GridOccupant occupant)
        {
            if (IdxIsOnGrid(occupant.Position))
            {
                _nodes[occupant.Position.x, occupant.Position.y].RemoveOccupant(occupant);
                occupant.Position = Vector2IntEx.OffGrid;
            }
        }

        public override Vector2Int MoveOccupant(Vector2 newWorldPos, GridOccupant occupant)
        {
            RemoveOccupant(occupant);

            return AddOccupant(newWorldPos, occupant);
        }

        public Vector2Int GetNodeIndexAt(Vector2 worldPosition)
        {
            var p = (worldPosition - _position);
            var idx = Mathf.FloorToInt(p.x);
            var idy = Mathf.FloorToInt(p.y);
            return new Vector2Int(idx, idy);
        }

        public bool IdxIsOnGrid(Vector2Int idx)
        {
            return (idx.x < _width && idx.x > -1) && (idx.y < _height && idx.y > -1);
        }

    }
}