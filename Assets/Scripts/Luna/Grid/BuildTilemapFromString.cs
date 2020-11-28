using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using Util.Events;
using Random = System.Random;

namespace Luna.Grid
{
    public class BuildTilemapFromString : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private TilePalette tiles;
        [SerializeField] private RoomTemplate room;
        [SerializeField] private Tilemap chasm;
        [SerializeField] private Tilemap floor;
        [SerializeField] private Tilemap walls;
        [SerializeField] private int width = 9;
        [SerializeField] private int height = 12;
        [SerializeField] private float timelapse;
        [SerializeField] private GridVariable output;
        [SerializeField] private VoidGameEvent onGridBuilt;

        [SerializeField] private RuleTile wallRule;
        [SerializeField] private GameObject unbreakableWall;
        [SerializeField] private GameObject chasmObject;

        [SerializeField] private Transform enemiesParent;
        [SerializeField] private Transform terrainParent;
        [SerializeField] private Transform chasmParent;
        [SerializeField] private Transform unbreakableParent;


        private string testLevelTop =
            "ccc000e00" +
            "cc00wwwww" +
            "c000w000w" +
            "e000wwwww" +
            "cc00wwwww" +
            "cc00wwwww";

        private string testLevelBottom =
            "cc000e000" +
            "c000ww0ww" +
            "00w0cc00e" +
            "00w0cc00w" +
            "e0w0000cc" +
            "000e00ccc";

        private void Start()
        {
            StartCoroutine(BuildRoom());
        }


        private IEnumerator BuildRoom()
        {
            var roomString = testLevelTop + testLevelBottom;
            var adjustedHeight = height + 4;
            var adjustedWidth = width + 4;
            // var roomString = room.Template;

            roomString = AddBorder(roomString);

            var grid = new Grid.Node[adjustedWidth, adjustedHeight];

            int tileIdx = 0;
            Vector3Int pos = Vector3Int.zero;
            Vector2 worldPos = Vector2.zero;

            for (int y = adjustedHeight - 1; y > -1; --y)
            {
                pos.y = y;
                worldPos.y = y + .5f;

                for (int x = 0; x < adjustedWidth; ++x)
                {
                    pos.x = x;
                    worldPos.x = x + .5f;

                    var stringIdx = x + adjustedWidth * ((adjustedHeight - 1) - y);


                    switch (roomString[stringIdx])
                    {
                        case 'c':
                        {
                            chasm.SetTile(pos, tiles.ChasmTiles.RandomElement());
                            var go = Instantiate(chasmObject, worldPos, Quaternion.identity);
                            go.transform.parent = chasmParent;
                            grid[x, y] = new Grid.Node(x, y, -2, worldPos);
                            break;
                        }
                        case 'u':
                        {
                            var go = Instantiate(unbreakableWall, worldPos, Quaternion.identity);
                            go.transform.parent = unbreakableParent;
                            grid[x, y] = new Grid.Node(x, y, -2, worldPos);
                            break;
                        }
                        case 'w':
                        {
                            walls.SetTile(pos, wallRule);
                            // walls.SetTile(pos, tiles.WallTiles.RandomElement());
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            // add in a wall object with gridOccupantBehaviour, it will add itself to the grid on initialise
                            // var wall = Instantiate(tiles.WallObject, worldPos, Quaternion.identity);
                            // wall.transform.parent = walls.transform;
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }
                        case 'e': // entry/exit
                        {
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }

                        default: // must be floor so can spawns ememies and loot here
                        {
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);

                            foreach (var enemy in tiles.Enemies)
                            {
                                if (UnityEngine.Random.Range(1, enemy.Second) == 1)
                                {
                                    var go = Instantiate(enemy.First, worldPos, Quaternion.identity);
                                    go.transform.parent = enemiesParent;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    if (timelapse > 0)
                    {
                        yield return new WaitForSeconds(timelapse);
                    }
                }
            }

            for (int i = roomString.Length - 1; i > -1; --i)
            {
                if (roomString[i] == 'e')
                {
                    var x = (i % adjustedWidth) + .5f;
                    var y = (adjustedHeight + 2 - 1) - (int) (i / adjustedWidth) + .5f;
                    Instantiate(player, new Vector3(x, y), Quaternion.identity);
                    break;
                }
            }

            output.Value = new SquareGrid(grid, Vector2.zero);
            yield return null;
            onGridBuilt.Raise();
            yield break;
        }

        private string AddBorder(string roomString)
        {
            var rows = new String[height + 4];
            var replaced = roomString.Substring(0, width).ToArray();
            for (int i = 0; i < replaced.Length; i++)
            {
                if (replaced[i] != 'c')
                    replaced[i] = 'u';
            }

            rows[0] = new string(replaced);
            rows[1] = new string(replaced);
            replaced = roomString.Substring(roomString.Length - width).ToArray();
            for (int i = 0; i < replaced.Length; i++)
            {
                if (replaced[i] != 'c')
                    replaced[i] = 'u';
            }

            rows[height + 2] = new string(replaced);
            rows[height + 3] = new string(replaced);

            for (int i = 0; i < height + 4; i++)
            {
                if (i > 1 && i < height + 2)
                {
                    rows[i] = roomString.Substring((i - 2) * width, width);
                }

                var start = rows[i][0] == 'c' ? 'c' : 'u';
                var end = rows[i][width - 1] == 'c' ? 'c' : 'u';
                rows[i] = rows[i].PadLeft(width + 2, start);
                rows[i] = rows[i].PadRight(width + 4, end);
            }


            return rows.Aggregate("", (prod, next) => prod + next);
        }
    }
}