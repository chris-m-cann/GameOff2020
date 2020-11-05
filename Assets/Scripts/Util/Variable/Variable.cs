using UnityEngine;

namespace Util.Variable
{
    public abstract class Variable<T> : ResetableObject
    {

        [SerializeField] private T current;
        [SerializeField] private T intialValue;
        [SerializeField] private bool resetOnSceneChange = false;

        public virtual T Value
        {
            get => current;
            set => current = value;
        }

        protected virtual void OnEnable()
        {
            if (resetOnSceneChange) Reset();
        }

        public override void Reset(ResetScenario scenario)
        {
            switch (scenario)
            {
                case ResetScenario.OnSceneUnload:
                    if (resetOnSceneChange) Reset();
                    break;
                case ResetScenario.OnDemand:
                    Reset();
                    break;
                default:
                    break;
            }
        }

        private void Reset()
        {
            current = intialValue;
        }
    }
}