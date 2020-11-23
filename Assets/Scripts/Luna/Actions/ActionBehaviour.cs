using UnityEngine;

namespace Luna.Actions
{
    public abstract class ActionBehaviour : MonoBehaviour, IUnitAction
    {
        public abstract void StartAction(Unit.Unit unit);

        public abstract bool Tick(Unit.Unit actor);

        public int Priority { get; }
    }
}