using UnityEngine;
using UnityEngine.Tilemaps;

namespace Luna.Grid
{
    [CreateAssetMenu(menuName = "Custom/TilePalette")]
    public class TilePalette : ScriptableObject
    {
        public Tile[] ChasmTiles;
        public Tile[] FloorTiles;
        public Tile[] WallTiles;
        public Tile[] EnemyTiles;
        public Tile[] TerrainTiles;
        public Tile[] PickupTiles;
        public Tile EndTile;

        public GridOccupantBehaviour WallObject;
    }
}