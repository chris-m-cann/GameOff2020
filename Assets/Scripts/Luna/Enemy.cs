using UnityEngine;

namespace Luna
{
    public class Enemy: TurnTaker
    {
        private MoveAlongPath _move;

        private bool _turnComplete = false;
        private bool _upNext;
        private void Awake()
        {
            _move = GetComponent<MoveAlongPath>();
        }

        public override void StartTurn()
        {
            _turnComplete = false;

            var nexPos = _upNext ? Vector3.up : Vector3.down;
            _upNext = !_upNext;
            nexPos += transform.position;

            _move.Move(nexPos, () => _turnComplete = true);
        }

        public override bool IsTurnFinished()
        {
            return _turnComplete;
        }
    }
}