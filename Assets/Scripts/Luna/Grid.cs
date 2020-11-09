using UnityEngine;

namespace Luna
{
    public abstract class Grid
    {
        public struct Node
        {
            public int cost;
            public int x, y;
            public Vector2 worldPosition;

            public Node(int x, int y, int cost, Vector2 worldPosition)
            {
                this.cost = cost;
                this.x = x;
                this.y = y;
                this.worldPosition = worldPosition;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is Node node)
                {
                    return cost == node.cost && x == node.x && y == node.y && worldPosition == node.worldPosition;
                }
                else
                {
                    return false;
                }
            }
        }

        public abstract bool TryGetNodeAt(int x, int y, ref Node node);
        public abstract bool TryGetNodeAtWorldPosition(Vector2 pos, ref Node node);
        public abstract Node[] GetNeighbours(Node node);
        public abstract bool HasTileAtPos(Vector2 target);
        public abstract Vector2 GetWorldPos(Node node);
    }
}
