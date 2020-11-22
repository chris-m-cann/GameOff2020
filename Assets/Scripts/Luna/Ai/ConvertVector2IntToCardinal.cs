using UnityEditor;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class ConvertVector2IntToCardinal : BtNode
    {
        [SerializeField] private BlackboardKey key;

        protected override State OnExecute(AgentContext context)
        {
            if (!context.AgentBlackboard.Contains(key)) return State.Failed;

            var vec = context.AgentBlackboard.RetrieveData<Vector2Int>(key);

            if (vec.x != 0 && vec.y != 0)
            {
                if (Mathf.Abs(vec.x) >= Mathf.Abs(vec.y))
                {
                    vec.y = 0;
                }
                else
                {
                    vec.x = 0;
                }
            }

            context.AgentBlackboard.Add(key, vec);

            return State.Succeeded;
        }
    }
}