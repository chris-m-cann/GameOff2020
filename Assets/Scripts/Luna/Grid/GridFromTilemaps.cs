using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util.Events;

namespace Luna.Grid
{
    public class GridFromTilemaps : MonoBehaviour
    {
        [SerializeField] private Tilemap impassable;
        [SerializeField] private Tilemap passable;

        [SerializeField] private int width = 20;
        [SerializeField] private int height = 12;
        [SerializeField] private Vector2Int bottomLeft;
        [SerializeField] private GridVariable output;
        [SerializeField] private VoidGameEvent onGridGenerated;


        private SquareGrid _grid;
        private void Start()
        {
            var offsetX = .5f;
            var offsety = .5f;

            Grid.Node[,] nodes = new Grid.Node[width, height];

            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    var worldPosition = new Vector3(x + bottomLeft.x + offsetX, y + bottomLeft.y + offsety);
                    var cellPos = impassable.WorldToCell(worldPosition);
                    var tile = impassable.GetTile(cellPos);

                    if (tile)
                    {
                        nodes[x, y] = new Grid.Node(x, y, -1, worldPosition);

                        sb.Append(", -1");

                        continue;
                    }


                    cellPos = impassable.WorldToCell(worldPosition);

                    tile = passable.GetTile(cellPos);

                    if (tile)
                    {
                        nodes[x, y] = new Grid.Node(x, y, 1, worldPosition);
                        sb.Append(",  1");
                        continue;
                    }

                    nodes[x, y] = new Grid.Node(x, y, -2, worldPosition);
                    sb.Append(", -2");
                }

                sb.Append("\n");
            }

         //   Debug.Log("grid:\n" + sb.ToString());

            _grid = new SquareGrid(nodes, bottomLeft);

            output.Value = _grid;
            onGridGenerated.Raise();
        }

        public Grid Provide()
        {
            return _grid;
        }
    }
}
