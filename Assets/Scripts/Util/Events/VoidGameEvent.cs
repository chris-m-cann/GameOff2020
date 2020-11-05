using UnityEngine;

namespace Util.Events
{
    [CreateAssetMenu(menuName = "GameEvents/Void")]
    public class VoidGameEvent : GameEvent<Void>
    {
        public void Raise() => Raise(Void.Instance);
    }
}