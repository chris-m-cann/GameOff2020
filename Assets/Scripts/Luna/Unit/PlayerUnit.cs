using System.Collections.Generic;
using Luna.Actions;
using UnityEngine;

namespace Luna.Unit
{
    [RequireComponent(typeof(PlayerInputAction))]
    public class PlayerUnit : BaseUnit
    {
        private PlayerInputAction _input;
        private void Awake()
        {
            _input = GetComponent<PlayerInputAction>();
        }

        public override List<IUnitAction> StartTurn()
        {
            _input.Reset();
            return new List<IUnitAction>(1) {_input};
        }
    }
}