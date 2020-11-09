using UnityEngine;

namespace Luna
{
    public abstract class TurnTaker : MonoBehaviour
    {
        public abstract void StartTurn();
        public abstract bool IsTurnFinished();
    }
}