using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using Util.Events;
using Random = UnityEngine.Random;

namespace Luna.Grid
{
    public class BuildTilemapFromString : MonoBehaviour
    {
        [SerializeField] private CurrentRunData run;

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


        private void Start()
        {
            StartCoroutine(BuildRoom());

        }


        private LevelDefinition PickRoom(out char playerEntrance, out bool flipX, out bool flipY)
        {
            flipX = false;
            flipY = false;

            if (run.LeftLastRoomBy == Vector2Int.right)
            {
                playerEntrance = 'l';
            }
            else if (run.LeftLastRoomBy == Vector2Int.left)
            {
                playerEntrance = 'r';
            }
            else if (run.LeftLastRoomBy == Vector2Int.down)
            {
                playerEntrance = 't';
            }
            else
            {
                playerEntrance = 'b';
            }

            for (int i = 0; i < 3; i++)
            {
                var idx = Random.Range(0, LevelData.Levels.Length);
                if (idx == run.LastRoomIdx) continue;

                var room = LevelData.Levels[idx];

                flipX = Random.value > .5f;
                flipY = Random.value > .5f;

                char entranceNeeded = playerEntrance;
                if (flipX)
                {
                    if (playerEntrance == 'r')
                    {
                        entranceNeeded = 'l';
                    } else if (playerEntrance == 'l')
                    {
                        entranceNeeded = 'r';
                    }
                }

                if (flipY)
                {
                    if (playerEntrance == 't')
                    {
                        entranceNeeded = 'b';
                    } else if (playerEntrance == 'b')
                    {
                        entranceNeeded = 't';
                    }
                }


                if (room.Level.Contains(entranceNeeded))
                {
                    run.LastRoomIdx = idx;
                    return room;
                }
            }

            run.LastRoomIdx = Random.Range(0, LevelData.Levels.Length);

            var backupRoom = LevelData.Levels[run.LastRoomIdx];
            playerEntrance = backupRoom.Level.Where(IsEntrance).ToArray().RandomElement();
            return backupRoom;
        }

        private char[,] FlipX(char[,] chars, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < (int)(width / 2); x++)
                {
                    if (chars[x, y] == 'l')
                    {
                        chars[x, y] = 'r';
                    }

                    char tmp = chars[x, y];
                    chars[x, y] = chars[width - 1 - x, y];
                    chars[width - 1 - x, y] = tmp;

                    if (chars[x, y] == 'r')
                    {
                        chars[x, y] = 'l';
                    }
                }
            }

            return chars;
        }

        private char[,] FlipY(char[,] chars, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < (int)(height / 2); y++)
                {
                    if (chars[x, y] == 'b')
                    {
                        chars[x, y] = 't';
                    }
                    char tmp = chars[x, y];
                    chars[x, y] = chars[x, height - 1 - y];
                    chars[x, height - 1 - y] = tmp;

                    if (chars[x, y] == 't')
                    {
                        chars[x, y] = 'b';
                    }
                }
            }

            return chars;
        }

        private bool IsEntrance(char c)
        {
            return (c == 't' || c == 'b' || c == 'l' || c == 'r');
        }


        private IEnumerator BuildRoom()
        {
            LevelDefinition level = new LevelDefinition();
            var adjustedHeight = height + 4;
            var adjustedWidth = width + 4;
            char[,] roomChars = new char[adjustedWidth, adjustedHeight];
            char playerEntrance = 'b';

            bool levelParsed = false;
            while (!levelParsed)
            {
                try
                {
                    level = PickRoom(out playerEntrance, out var flipX, out var flipY);
                    var roomString = level.Level;
                    // var roomString = room.Template;

                    roomChars = StringToCharTable(width, height, roomString);

                    roomChars = ExpandChunks(roomChars);

                    roomChars = AddBorder(roomChars);

                    if (flipX)
                    {
                        roomChars = FlipX(roomChars, adjustedWidth, adjustedHeight);
                    }

                    if (flipY)
                    {
                        roomChars = FlipY(roomChars, adjustedWidth, adjustedHeight);
                    }


                    levelParsed = true;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    // without this we have no game, pick a a new room and try again
                }
            }

            var grid = new Grid.Node[adjustedWidth, adjustedHeight];

            int tileIdx = 0;
            Vector3Int pos = Vector3Int.zero;
            Vector2 worldPos = Vector2.zero;
            bool playerSpawned = false;

            var enemySpawnPoints = new List<Vector3Int>();
            var terrainSpawnPoints = new List<Vector3Int>();
            var lootSpawnPoints = new List<Vector3Int>();
            var specialLootSpawnPoints = new List<Vector3Int>();

            for (int y = 0; y < adjustedHeight; ++y)
            {
                pos.y = y;
                worldPos.y = y + .5f;

                for (int x = 0; x < adjustedWidth; ++x)
                {
                    pos.x = x;
                    worldPos.x = x + .5f;


                    switch (roomChars[x, y])
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
                        case 'h': // high value item
                        {
                            specialLootSpawnPoints.Add(pos);
                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }
                        default: // must be floor so can spawns ememies and loot here
                        {
                            enemySpawnPoints.Add(pos);
                            terrainSpawnPoints.Add(pos);
                            lootSpawnPoints.Add(pos);

                            floor.SetTile(pos, tiles.FloorTiles.RandomElement());
                            grid[x, y] = new Grid.Node(x, y, 1, worldPos);
                            break;
                        }
                    }

                    if (timelapse > 0)
                    {
                        yield return new WaitForSeconds(timelapse);
                    }
                }
            }

            var enemiesAmount = UnityEngine.Random.Range(level.MinEnemies, level.MaxEnemies + 1);
            var terrainAmount = UnityEngine.Random.Range(level.MinTerrain, level.MaxTerrain + 1);
            var itemsAmount = UnityEngine.Random.Range(level.MinItems, level.MaxItems + 1);

            while (enemySpawnPoints.Count > 0 && enemiesAmount > 0)
            {
                var point = enemySpawnPoints.RandomElement();
                worldPos = new Vector2(point.x + .5f, point.y + .5f);

                var spawned = false;
                foreach (var enemy in tiles.Enemies)
                {
                    if (UnityEngine.Random.Range(1, enemy.Second) == 1)
                    {
                        var go = Instantiate(enemy.First, worldPos, Quaternion.identity);
                        go.transform.parent = enemiesParent;
                        spawned = true;
                        break;
                    }
                }

                if (spawned)
                {
                    --enemiesAmount;
                    enemySpawnPoints.Remove(point);
                }
            }


            while (terrainSpawnPoints.Count > 0 && terrainAmount > 0)
            {
                var point = terrainSpawnPoints.RandomElement();
                worldPos = new Vector2(point.x + .5f, point.y + .5f);

                var spawned = false;
                foreach (var terrain in tiles.Terrain)
                {
                    if (UnityEngine.Random.Range(1, terrain.Second) == 1)
                    {
                        var go = Instantiate(terrain.First, worldPos, Quaternion.identity);
                        go.transform.parent = terrainParent;
                        spawned = true;
                        break;
                    }
                }

                if (spawned)
                {
                    --terrainAmount;
                    terrainSpawnPoints.Remove(point);
                }
            }

            while (lootSpawnPoints.Count > 0 && itemsAmount > 0)
            {
                var point = lootSpawnPoints.RandomElement();
                worldPos = new Vector2(point.x + .5f, point.y + .5f);

                var spawned = false;
                foreach (var item in tiles.Items)
                {
                    if (UnityEngine.Random.Range(1, item.Second) == 1)
                    {
                        var go = Instantiate(tiles.Pickup, worldPos, Quaternion.identity);
                        // go.transform.parent = enemiesParent;
                        go.GetComponent<PickUpBehaviour>().SetItem(item.First);

                        spawned = true;
                        break;
                    }
                }

                if (spawned)
                {
                    --itemsAmount;
                    lootSpawnPoints.Remove(point);
                }
            }



            output.Value = new SquareGrid(grid, Vector2.zero);
            yield return null;
            onGridBuilt.Raise();
            yield break;
        }

        private char[,] ExpandChunks(char[,] roomChars)
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (roomChars[x, y] == LevelData.CHUNK_CHAR)
                    {
                        var chunk = LevelData.Chunks.RandomElement();
                        var chunkChars = StringToCharTable(LevelData.CHUNK_WIDTH, LevelData.CHUNK_HEIGHT, chunk);

                        if (Random.value > .5f)
                        {
                            chunkChars = FlipX(chunkChars, LevelData.CHUNK_WIDTH, LevelData.CHUNK_HEIGHT);
                        }

                        if (Random.value > .5f)
                        {
                            chunkChars = FlipY(chunkChars, LevelData.CHUNK_WIDTH, LevelData.CHUNK_HEIGHT);
                        }

                        for (int chunkY = 0; chunkY < LevelData.CHUNK_HEIGHT; ++chunkY)
                        {
                            for (int chunkX = 0; chunkX < LevelData.CHUNK_WIDTH; ++chunkX)
                            {
                                var finalX = x + chunkX;
                                var finalY = y + chunkY;
                                roomChars[finalX, finalY] = chunkChars[chunkX, chunkY];
                            }
                        }
                    }
                }
            }

            return roomChars;
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

        private char[,] AddBorder(char[,] roomChars)
        {
            var newWidth = width + 4;
            var newHeight = height + 4;
            var output = new char[newWidth, newHeight];

            for (int i = 0; i < width; i++)
            {
                switch (roomChars[i, 0])
                {
                    case 'c':
                        output[i + 2, 1] = 'c';
                        output[i + 2, 0] = 'c';
                        break;
                    case 'b':
                        output[i + 2, 1] = 'n';
                        output[i + 2, 0] = 'u';
                        break;
                    default:
                        output[i + 2, 1] = 'u';
                        output[i + 2, 0] = 'u';
                        break;
                }
            }


            for (int i = 0; i < width; i++)
            {
                switch (roomChars[i, height - 1])
                {
                    case 'c':
                        output[i + 2, newHeight - 1] = 'c';
                        output[i + 2, newHeight - 2] = 'c';
                        break;
                    case 't':
                        output[i + 2, newHeight - 1] = 'u';
                        output[i + 2, newHeight - 2] = 'n';
                        break;
                    default:
                        output[i + 2, newHeight - 1] = 'u';
                        output[i + 2, newHeight - 2] = 'u';
                        break;
                }
            }

            for (int i = 0; i < newHeight; i++)
            {
                if (i > 1 && i < height + 2)
                {
                    for (int x = 0; x < width; x++)
                    {
                        output[x + 2, i] = roomChars[x, i - 2];
                    }
                }

                switch (output[2, i])
                {
                    case 'l':
                        output[0, i] = 'u';
                        output[1, i] = 'n';
                        break;
                    case 'c':
                        output[0, i] = 'c';
                        output[1, i] = 'c';
                        break;
                    default:
                        output[0, i] = 'u';
                        output[1, i] = 'u';
                        break;
                }

                switch (output[newWidth - 3, i])
                {
                    case 'r':
                        output[newWidth - 2, i] = 'n';
                        output[newWidth - 1, i] = 'u';
                        break;
                    case 'c':
                        output[newWidth - 2, i] = 'c';
                        output[newWidth - 1, i] = 'c';
                        break;
                    default:
                        output[newWidth - 2, i] = 'u';
                        output[newWidth - 1, i] = 'u';
                        break;
                }
            }


            return output;
        }

        static char[,] StringToCharTable(int width, int height, string str)
        {
            if (str.Length < (width * height))
            {
                Debug.LogError($"str was too for small for {width}x{height}. only {str.Length}");
                throw new ArgumentOutOfRangeException($"str was too for small for {width}x{height}. only {str.Length}");
            }

            var table = new char[width, height];
            for (int y = height - 1; y > -1; --y)
            {
                for (int x = 0; x < width; ++x)
                {
                    var stringIdx = x + width * ((height - 1) - y);
                    table[x, y] = str[stringIdx];
                }
            }

            return table;
        }
    }
}