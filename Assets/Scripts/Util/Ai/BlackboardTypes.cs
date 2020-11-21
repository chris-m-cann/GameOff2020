using System;
using Ai;
using UnityEngine;
using Object = System.Object;

namespace Util.Ai
{
    [Serializable]
    public class BlackboardTypes
    {
        public ScriptableObject ScriptableObject;
        public Vector2Int Vector2Int;
        public float Float;

        // needs to be serialzed for property draw
        [SerializeField] private int option;
        public void AddToBlackboard(Blackboard board, BlackboardKey key)
        {
            switch (option)
            {
                case 0:
                    board.Add(key, ScriptableObject);
                    break;
                case 1:
                    board.Add(key, Vector2Int);
                    break;
                case 2:
                    board.Add(key, Float);
                    break;
            }
        }
    }
}