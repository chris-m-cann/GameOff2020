using System;
using Luna.Grid;
using UnityEngine;
using Util.Events;

namespace Luna
{
    public class TurnActionController
    {
        // bool true => action complete too
        public event Action<bool> OnTurnCompleted;
        public GameObject Actor;
        public GridVariable Grid;

        public TurnActionController(GameObject actor, GridVariable grid)
        {
            Actor = actor;
            Grid = grid;
        }

        public void OnTurnComplete(bool actionComplete)
        {
            OnTurnCompleted?.Invoke(actionComplete);
        }
    }
}