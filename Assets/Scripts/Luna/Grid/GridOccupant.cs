using System;
using System.Linq;
using UnityEngine;

namespace Luna.Grid
{
    [Serializable]
    public class GridOccupant
    {
        public GridOccupantTag[] Tags;
        public int Cost;
        public bool IsCollectable;
        [HideInInspector] public GameObject OccupantGameObject;
    }
}