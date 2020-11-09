using UnityEngine;

namespace Luna
{
    public class SquareGridVisualiser : MonoBehaviour
    {
        [SerializeField] private GridVariable grid;

        private void OnDrawGizmosSelected()
        {
            if (grid == null || grid.Value == null) return;

            if (!(grid.Value is SquareGrid squareGrid)) return;

            var offsetX = .5f; // half a grid square to get us into he center
            var offsety = .5f; // half a grid square to get us into he center

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
                    Grid.Node n = new Grid.Node();
                    if (squareGrid.TryGetNodeAt(x, y, ref n))
                    {
                        switch (n.cost)
                        {
                            case -2:
                            {
                                Gizmos.color = none;
                                break;
                            }
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
                        }

                        Gizmos.DrawCube(n.worldPosition, Vector3.one);
                    }
                }
            }
        }

    }
}