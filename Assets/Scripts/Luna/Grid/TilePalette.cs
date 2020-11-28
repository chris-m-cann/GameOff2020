using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using Util.Inventory;

namespace Luna.Grid
{
    [CreateAssetMenu(menuName = "Custom/TilePalette")]
    public class TilePalette : ScriptableObject
    {
        public Tile[] ChasmTiles;
        public Tile[] FloorTiles;
        public OccupantChancePair[] Enemies;
        public OccupantChancePair[] Terrain;
        public ItemChancePair[] Items;
        public GridOccupantBehaviour End;
        public RuleTile Wall;
        public GridOccupantBehaviour UnbreakableWall;
        public GridOccupantBehaviour Chasm;
        public PickUpBehaviour Pickup;

        public GridOccupantBehaviour TopExit;
        public GridOccupantBehaviour BottomExit;
        public GridOccupantBehaviour LeftExit;
        public GridOccupantBehaviour RightExit;


        [Serializable] public class OccupantChancePair : Pair<GridOccupantBehaviour, int>{}
        [Serializable] public class ItemChancePair : Pair<InventoryItem, int>{}
    }
}