using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using Util.Events;

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

        private string testLevelTop =
            "ccc000e00" +
            "cc000000w" +
            "c000w000w" +
            "e000wwwww" +
            "cc00www0e" +
            "cc000e000";
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
            // var roomString = room.Template;

            var grid = new Grid.Node[width, height];

            int tileIdx = 0;
            Vector3Int pos = Vector3Int.zero;
            Vector2 worldPos = Vector2.zero;
            for (int y = height - 1; y > -1; --y)
            {
                pos.y = y;
                worldPos.y = y + .5f;

                for (int x = 0; x < width; ++x)
                {
                    pos.x = x;
                    worldPos.x = x + .5f;

                    var stringIdx = x + width * ((height - 1) - y);


                    switch (roomString[stringIdx])
                    {
                        case 'c' :
                            chasm.SetTile(pos, tiles.ChasmTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, -2, worldPos);
                            break;
                        case 'w':
                            walls.SetTile(pos, tiles.WallTiles.RandomElement());
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            // add in a wall object with gridOccupantBehaviour, it will add itself to the grid on initialise
                            var wall = Instantiate(tiles.WallObject, worldPos, Quaternion.identity);
                            wall.transform.parent = walls.transform;
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        default:
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
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
                    var x = (i % width) + .5f;
                    var y = (height - 1) - (int)(i / width) + .5f;
                    Instantiate(player, new Vector3(x, y), Quaternion.identity);
                    break;
                }
            }

            output.Value = new SquareGrid(grid, Vector2.zero);
            onGridBuilt.Raise();
            yield break;
        }
    }
}