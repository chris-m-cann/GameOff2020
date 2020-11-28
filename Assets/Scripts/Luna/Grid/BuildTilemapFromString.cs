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

        [SerializeField] private Transform enemiesParent;
        [SerializeField] private Transform terrainParent;
        [SerializeField] private Transform chasmParent;
        [SerializeField] private Transform unbreakableParent;


        private string testLevelTop =
            "ccc000tc0" +
            "cc00wwwww" +
            "c000w000w" +
            "l000wwwww" +
            "cc00wwwww" +
            "cc00wwwww";

        private string testLevelBottom =
            "cc0000000" +
            "c000ww0ww" +
            "00w0cc00r" +
            "00w0cc00w" +
            "l0w0000cc" +
            "000b00ccc";

        private void Start()
        {
            StartCoroutine(BuildRoom('b'));
        }


        private IEnumerator BuildRoom(char playerEntrance)
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
            bool playerSpawned = false;

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
                            var go = Instantiate(tiles.Chasm, worldPos, Quaternion.identity);
                            go.transform.parent = chasmParent;
                            grid[x, y] = new Grid.Node(x, y, -2, worldPos);
                            break;
                        }
                        case 'u':
                        {
                            var go = Instantiate(tiles.UnbreakableWall, worldPos, Quaternion.identity);
                            go.transform.parent = unbreakableParent;
                            grid[x, y] = new Grid.Node(x, y, -2, worldPos);
                            break;
                        }
                        case 'w':
                        {
                            walls.SetTile(pos, tiles.Wall);
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }
                        case 'b': // entry/exit
                        {
                            playerSpawned = SpawnEntrance(
                                    'b',
                                    playerEntrance,
                                    playerSpawned,
                                    worldPos,
                                    Vector2.down,
                                    tiles.BottomExit
                                    );

                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }

                        case 'l': // entry/exit
                        {
                            playerSpawned = SpawnEntrance(
                                'l',
                                playerEntrance,
                                playerSpawned,
                                worldPos,
                                Vector2.left,
                                tiles.LeftExit
                                );

                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }

                        case 't': // entry/exit
                        {
                            playerSpawned = SpawnEntrance(
                                    't',
                                    playerEntrance,
                                    playerSpawned,
                                    worldPos,
                                    Vector2.up,
                                    tiles.TopExit
                                    );

                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }

                        case 'r': // entry/exit
                        {
                            playerSpawned = SpawnEntrance(
                                'r',
                                playerEntrance,
                                playerSpawned,
                                worldPos,
                                Vector2.right,
                                tiles.RightExit
                            );

                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }
                        case 'n': // entry/exit
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

                            foreach (var item in tiles.Items)
                            {
                                if (UnityEngine.Random.Range(1, item.Second) == 1)
                                {
                                    var go = Instantiate(tiles.Pickup, worldPos, Quaternion.identity);
                                    // go.transform.parent = enemiesParent;
                                    go.GetComponent<PickUpBehaviour>().SetItem(item.First);

                                    break;
                                }
                            }

                            foreach (var terrain in tiles.Terrain)
                            {
                                if (UnityEngine.Random.Range(1, terrain.Second) == 1)
                                {
                                    var go = Instantiate(terrain.First, worldPos, Quaternion.identity);
                                    go.transform.parent = terrainParent;
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

            output.Value = new SquareGrid(grid, Vector2.zero);
            yield return null;
            onGridBuilt.Raise();
            yield break;
        }

        private bool SpawnEntrance(
            char entryType,
            char playerEntrance,
            bool playerSpawned,
            Vector2 worldPos,
            Vector2 behindPos,
            GridOccupantBehaviour entrance)
        {
            var ret = playerSpawned;
            if (playerEntrance == entryType && !playerSpawned)
            {
                Instantiate(player, worldPos, Quaternion.identity);

                ret = true;
            }
            else
            {

                var entrancePos = worldPos + behindPos;
                var go = Instantiate(entrance, entrancePos, Quaternion.identity);

                go.transform.parent = terrainParent;
            }

            return ret;
        }

        private string AddBorder(string roomString)
        {
            var rows = new String[height + 4];
            var replaced = roomString.Substring(0, width).ToArray();
            var replaced2 = replaced;
            for (int i = 0; i < replaced.Length; i++)
            {
                switch (replaced[i])
                {
                    case 'c':
                        break;
                    case 't':
                        replaced[i] = 'u';
                        replaced2[i] = 'n';
                        break;
                    default:
                        replaced[i] = 'u';
                        replaced2[i] = 'u';
                        break;
                }
            }

            rows[0] = new string(replaced);
            rows[1] = new string(replaced2);
            replaced = roomString.Substring(roomString.Length - width).ToArray();

            for (int i = 0; i < replaced.Length; i++)
            {
                switch (replaced[i])
                {
                    case 'c':
                        break;
                    case 'b':
                        replaced[i] = 'u';
                        replaced2[i] = 'n';
                        break;
                    default:
                        replaced[i] = 'u';
                        replaced2[i] = 'u';
                        break;
                }
            }

            rows[height + 2] = new string(replaced2);
            rows[height + 3] = new string(replaced);

            for (int i = 0; i < height + 4; i++)
            {
                if (i > 1 && i < height + 2)
                {
                    rows[i] = roomString.Substring((i - 2) * width, width);
                }

                string first;
                switch (rows[i][0])
                {
                    case 'l' :
                        first = "u0";
                        break;
                    case 'c':
                        first = "cc";
                        break;
                    default:
                        first = "uu";
                        break;
                }

                string last;
                switch (rows[i][width -1])
                {
                    case 'r' :
                        last = "nu";
                        break;
                    case 'c':
                        last = "cc";
                        break;
                    default:
                        last = "uu";
                        break;
                }


                rows[i] = first + rows[i];
                rows[i] = rows[i] + last;
            }


            return rows.Aggregate("", (prod, next) => prod + next);
        }
    }
}