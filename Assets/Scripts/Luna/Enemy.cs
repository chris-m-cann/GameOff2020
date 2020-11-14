using UnityEngine;

namespace Luna
{
    public class Enemy: MonoBehaviour, ITurnTaker
    {
        private bool _turnComplete = false;
        private bool _upNext;

        private MoveAlongPath _move;

        private void Awake()
        {
            _move = GetComponent<MoveAlongPath>();
        }

        public void StartTurn()
        {
            _turnComplete = false;

            var nexPos = _upNext ? Vector3.up : Vector3.down;
            _upNext = !_upNext;
            nexPos += transform.position;

            _move.Move(nexPos, () => _turnComplete = true);
        }

        public bool IsTurnFinished()
        {
            return _turnComplete;
        }
    }
}