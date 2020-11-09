using UnityEngine;

namespace Luna
{
    public interface ITurnAction
    {
        void Run(TurnActionController actor);
        void Cancel(TurnActionController actor);
    }
}