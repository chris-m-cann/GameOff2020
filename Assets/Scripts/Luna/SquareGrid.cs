using System;
using UnityEngine;

namespace Luna
{
    public class SquareGrid : Grid
    {
        private Node[,] _nodes;
        private int width;
        private int height;
        private Vector2 position;

        public int Width => width;
        public int Height => height;

        public SquareGrid(Node[,] nodes, Vector2 positionOf00)
        {
            _nodes = nodes;
            width = nodes.GetLength(0);
            height = nodes.GetLength(1);
            position = positionOf00;
        }

        public override bool TryGetNodeAt(int x, int y, ref Node node)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return false;
            node = _nodes[x, y];
            return true;
        }

        public override bool TryGetNodeAtWorldPosition(Vector2 pos, ref Node node)
        {
            var p = pos - position;
            var idx = Mathf.FloorToInt(p.x);
            var idy = Mathf.FloorToInt(p.y);

            if (idx >= width || idx < 0 && idy >= height && idy < 0)
            {
                return false;
            }

            return TryGetNodeAt(idx, idy, ref node);
        }

        public override Node[] GetNeighbours(Node node)
        {
            Node[] neighbours = new Node[4];
            int count = 0;

            if (node.x > 0)
            {
                neighbours[count] = _nodes[node.x - 1, node.y];
                count++;
            }

            if (node.y > 0)
            {
                neighbours[count] = _nodes[node.x, node.y - 1];
                count++;
            }

            if (node.x < (width - 1))
            {
                neighbours[count] = _nodes[node.x + 1, node.y];
                count++;
            }


            if (node.y < (height - 1))
            {
                neighbours[count] = _nodes[node.x, node.y + 1];
                count++;
            }


            Array.Resize(ref neighbours, count);
            return neighbours;
        }

        public override bool HasTileAtPos(Vector2 target)
        {
            var p = (target - position);
            var idx = Mathf.FloorToInt(p.x);
            var idy = Mathf.FloorToInt(p.y);

            return (idx < width && idx > -1) && (idy < height && idy > -1);
        }

        public override Vector2 GetWorldPos(Node node)
        {
            return new Vector2(node.x + position.x + .5f, node.y + position.y + .5f);
        }
    }
}