using UnityEngine;

namespace Luna.Grid
{
    public class SquareGridVisualiser : MonoBehaviour
    {
        [SerializeField] private GridVariable grid;
        [SerializeField] private bool printCosts;

        private void OnDrawGizmosSelected()
        {
            if (grid == null || grid.Value == null) return;

            if (!(grid.Value is SquareGrid squareGrid)) return;

            var walkable = Color.green;
            walkable.a = .5f;

            var blockers = Color.red;
            blockers.a = .5f;

            var none = Color.black;
            none.a = .5f;

            for (int x = 0; x < squareGrid.Width; x++)
            {
                for (int y = 0; y < squareGrid.Height; y++)
                {
                    Luna.Grid.Grid.Node n = new Luna.Grid.Grid.Node();
                    if (squareGrid.TryGetNodeAt(x, y, ref n))
                    {
                        if (printCosts)
                        {
                            Debug.Log($"({x},{y}) cost = {n.Cost}");
                        }
                        switch (n.Cost)
                        {
                            case -1:
                            {
                                Gizmos.color = blockers;
                                break;
                            }
                            case 1:
                            {
                                Gizmos.color = walkable;
                                break;
                            }
                            default:
                            {
                                Gizmos.color = none;
                                break;
                            }
                        }

                        Gizmos.DrawCube(n.WorldPosition, Vector3.one);
                    }
                }
            }
        }

    }
}