using System;
using UnityEngine;

namespace Util.Ai.Bt
{
    public class WriteToBlackboardNode<T> : BtDecoratorNode where T : IComparable
    {
        [SerializeField] private BlackboardKey key;
        [SerializeField] private T value;


        protected override State OnExecute(AgentContext context)
        {
            context.AgentBlackboard.Add(key, value);

            return child.Execute(context);
        }
    }
}