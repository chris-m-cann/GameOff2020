using UnityEngine;

namespace Luna
{
    public interface ITurnTaker
    {
        void StartTurn();
        bool IsTurnFinished();
    }
}