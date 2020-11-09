using System;
using UnityEngine;
using Util.Events;

namespace Luna
{
    public class TurnActionController
    {
        // bool true => action complete too
        public event Action<bool> OnTurnCompleted;
        public GameObject Actor;

        public TurnActionController(GameObject actor)
        {
            Actor = actor;
        }

        public void OnTurnComplete(bool actionComplete)
        {
            OnTurnCompleted?.Invoke(actionComplete);
        }
    }
}