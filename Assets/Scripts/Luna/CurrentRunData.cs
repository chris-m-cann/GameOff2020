using System;
using UnityEngine;

namespace Luna
{
    [CreateAssetMenu(menuName = "Custom/CurrentRunData")]
    public class CurrentRunData : ScriptableObject
    {
        [NonSerialized]
        public int Depth = 1;
        [NonSerialized]
        public int Score;
        [NonSerialized]
        public Vector2Int LeftLastRoomBy;
        [NonSerialized]
        public int LastRoomIdx = -1;

        public string GetDepthDisplay() => "Depth " + Depth;

        public void Reset()
        {
            Depth = 1;
            Score = 0;
            LeftLastRoomBy = Vector2Int.up;
        }
    }
}