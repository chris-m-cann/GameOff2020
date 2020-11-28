using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Luna.Grid
{
    [CreateAssetMenu(menuName = "Custom/TilePalette")]
    public class TilePalette : ScriptableObject
    {
        public Tile[] ChasmTiles;
        public Tile[] FloorTiles;
        public OccupantChancePair[] Enemies;
        public OccupantChancePair[] Terrain;
        public OccupantChancePair[] Pickups;
        public GridOccupantBehaviour End;

        [Serializable]
        public class OccupantChancePair : Pair<GridOccupantBehaviour, int>
        {

        }
    }
}