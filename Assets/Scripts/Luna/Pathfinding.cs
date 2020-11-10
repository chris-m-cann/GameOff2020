using System;
using System.Collections.Generic;
using Luna.Grid;
using UnityEngine;
using Util;

namespace Luna
{
    public class Pathfinding : MonoBehaviour
    {
        [SerializeField] private GridVariable grid;

        public List<Grid.Grid.Node> CalculatePath(Vector2 from, Vector2 to)
        {

            var startNode = new Grid.Grid.Node();
            var endNode = new Grid.Grid.Node();

            if (!grid.Value.TryGetNodeAtWorldPosition(from, ref startNode)) return new List<Grid.Grid.Node>();
            if (!grid.Value.TryGetNodeAtWorldPosition(to, ref endNode)) return new List<Grid.Grid.Node>();


            return CalculatePath(grid.Value, startNode, endNode);
        }

        private List<Grid.Grid.Node> CalculatePath(Grid.Grid grid, Grid.Grid.Node start, Grid.Grid.Node end)
        {
            var path = new List<Grid.Grid.Node>();

            var frontier = new PriorityQueue<Grid.Grid.Node, int>();
            var cameFrom = new Dictionary<Grid.Grid.Node, Grid.Grid.Node>();
            var costSoFar = new Dictionary<Grid.Grid.Node, int>();

            frontier.Enqueue(start, 0);
            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(end)) break;

                var costToCurrent = costSoFar[current];
                foreach (var next in grid.GetNeighbours(current))
                {
                    if (next.Cost < 0) continue;

                    var cost = costToCurrent + next.Cost;

                    if (costSoFar.ContainsKey(next) && cost >= costSoFar[next]) continue;

                    costSoFar[next] = cost;
                    int priority = cost + Heuristic(next, end);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }

            if (frontier.Count == 0) return path;

            var n = end;

            while (!n.Equals(start))
            {
                path.Add(n);
                n = cameFrom[n];
            }

            path.Reverse();
            return path;
        }

        private int Heuristic(Grid.Grid.Node node, Grid.Grid.Node end)
        {
            return Math.Abs(node.X - end.X) + Math.Abs(node.Y - end.Y);
        }
    }
}