using Ai;
using UnityEngine;

namespace Util.Ai.Bt
{
    public class HasLineOfSight2D : BtNode
    {
        private Blackboard.ElementKey _sensorKey;
        private LayerMask _layers;
        private float _range;

        public HasLineOfSight2D(Blackboard.ElementKey sensorKey, LayerMask layers, float range)
        {
            _sensorKey = sensorKey;
            _layers = layers;
            _range = range;
        }

        public override State Execute(AgentContext context)
        {
            var sensor = context.AgentBlackboard.RetrieveData<Transform>(_sensorKey);
            var target = context.Target;

            if (sensor == null || target == null) return State.Failed;
            var hit = Physics2D.Raycast(sensor.position, target.position - sensor.position, _range, _layers);

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