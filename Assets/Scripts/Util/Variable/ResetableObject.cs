using UnityEngine;

namespace Util
{
    public abstract class ResetableObject: ScriptableObject
    {
        public abstract void Reset(ResetScenario scenario = ResetScenario.OnDemand);
    }
}