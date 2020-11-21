using UnityEngine;

namespace Util.Ai.Bt
{
    public class RotateVector2IntNode : BtNode
    {
        [SerializeField] private BlackboardKey vector2IntKey;
        [SerializeField] private Vector3 rotation;


        protected override State OnExecute(AgentContext context)
        {
            if (!context.AgentBlackboard.Contains(vector2IntKey)) return State.Failed;

            var v2I = context.AgentBlackboard.RetrieveData<Vector2Int>(vector2IntKey);
            var v3 = new Vector3(v2I.x, v2I.y);
            v3 = Quaternion.Euler(rotation) * v3;

            context.AgentBlackboard.Add(vector2IntKey, v3.ToVector2Int());

            return State.Succeeded;
        }
    }
}