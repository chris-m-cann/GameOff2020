using System;
using System.Linq;
using UnityEngine;

namespace Luna.Grid
{
    [Serializable]
    public class GridOccupant
    {
        public GridOccupantType Type;
        public int Cost;
        public bool IsCollectable;
        [HideInInspector] public GameObject OccupantGameObject;
        [HideInInspector] public Vector2Int Position;
    }
}