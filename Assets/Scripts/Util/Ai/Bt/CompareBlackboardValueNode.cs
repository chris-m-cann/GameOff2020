using System;
using UnityEngine;

namespace Util.Ai.Bt
{
    public class CompareBlackboardValueNode<T> : BtNode where T : IComparable
    {
        [SerializeField] private BlackboardKey key;
        [SerializeField] private T rhs;
        [SerializeField] private CompareType comparison;


        protected override State OnExecute(AgentContext context)
        {
            if (!context.AgentBlackboard.Contains(key)) return State.Failed;

            var entry = context.AgentBlackboard.RetrieveData<T>(key);

            var compare = entry.CompareTo(rhs);
            switch (comparison)
            {
                case CompareType.LessThan:
                    return compare == -1 ? State.Succeeded : State.Failed;
                case CompareType.Equal:
                    return compare == 0 ? State.Succeeded : State.Failed;
                case CompareType.GreaterThan:
                    return compare == 1 ? State.Succeeded : State.Failed;
            }

            return State.Failed;
        }
    }
}