using Ai;
using UnityEngine;

namespace Util.Ai.Bt
{
    public class HasLineOfSight2D : BtNode
    {
        [SerializeField] private Blackboard.ElementKey sensorKey;
        [SerializeField] private LayerMask layers;
        [SerializeField] private float range;

        public HasLineOfSight2D() {}

        public HasLineOfSight2D(Blackboard.ElementKey sensorKey, LayerMask layers, float range)
        {
            this.sensorKey = sensorKey;
            this.layers = layers;
            this.range = range;
        }

        public override State Execute(AgentContext context)
        {
            var sensor = context.AgentBlackboard.RetrieveData<Transform>(sensorKey);
            var target = context.Target;

            if (sensor == null || target == null) return State.Failed;
            var hit = Physics2D.Raycast(sensor.position, target.position - sensor.position, range, layers);

            if (hit.collider?.transform == target)
            {
                return State.Succeeded;
            }
            else
            {
                return State.Failed;
            }
        }
    }
}